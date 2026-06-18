using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ReservaController : ControllerBase
    {
        ReservaLogica reservaLogica = new ReservaLogica();
        PagoLogica pagoLogica = new PagoLogica();
        ClienteLogica clienteLogica = new ClienteLogica();
        PaqueteLogica paqueteLogica = new PaqueteLogica();
        DestinoLogica destinoLogica = new DestinoLogica();
        private readonly EmailService _emailService;

        public ReservaController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult get()
        {
            List<ReservaModel> listaResultado = reservaLogica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            ReservaModel res = reservaLogica.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult post(ReservaModel request)
        {
            ReservaModel response = reservaLogica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult put(ReservaModel request)
        {
            ReservaModel response = reservaLogica.ActualizarRegistro(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = reservaLogica.deleteRegistro(id);
            return Ok(response);
        }

        [HttpPut("estatus")]
        public IActionResult putEstatus(ReservaEstatusRequest request)
        {
            ReservaModel reservaActual = reservaLogica.ObtenerPorId(request.ID_Reserva);
            if (reservaActual == null)
                return NotFound(new { mensaje = "Reserva no encontrada" });

            reservaActual.Estatus = request.Estatus;
            reservaLogica.ActualizarRegistro(reservaActual);

            bool correoEnviado = false;
            if (request.Estatus == "Cancelado")
            {
                try
                {
                    _emailService.EnviarCancelacion(
                        destinatario: request.CorreoCliente,
                        nombreCliente: request.NombreCliente,
                        nombrePaquete: request.NombrePaquete,
                        destino: request.Destino,
                        fechaInicio: request.FechaInicio,
                        fechaFin: request.FechaFin,
                        precioTotal: reservaActual.Precio_Total,
                        moneda: request.Moneda,
                        numeroPersonas: reservaActual.Numero_Personas
                    );
                    correoEnviado = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar correo de cancelación: {ex.Message}");
                }
            }

            return Ok(new { mensaje = "Estatus actualizado", correoEnviado });
        }

        [HttpPost("confirmar")]
        public async Task<IActionResult> ConfirmarReserva([FromBody] ReservaConfirmacionRequest request)
        {
            try
            {
                // Validación explícita del modelo
                if (!ModelState.IsValid)
                {
                    var errores = ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .ToDictionary(
                            kv => kv.Key,
                            kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        );

                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "Errores de validación",
                        errores = errores
                    });
                }

                if (request == null)
                    return BadRequest(new { success = false, mensaje = "Datos inválidos" });

                // Validación básica para acompañantes (solo nombre requerido)
                if (request.Acompanantes != null && request.Acompanantes.Any())
                {
                    foreach (var acompanante in request.Acompanantes)
                    {
                        if (string.IsNullOrWhiteSpace(acompanante.Nombre))
                            return BadRequest(new { success = false, mensaje = "Nombre de acompañante requerido" });
                    }
                }

                // 1. Crear la reserva
                var reserva = new ReservaModel
                {
                    ID_Cliente = request.ID_Cliente,
                    ID_Paquete = request.ID_Paquete,
                    Fecha_Reserva = request.Fecha_Reserva,
                    Numero_Personas = request.Numero_Personas,
                    Precio_Total = request.Precio_Total,
                    Estatus = "Pagado",
                    Observaciones = request.Observaciones ?? ""
                };

                ReservaModel reservaCreada = reservaLogica.CrearRegistro(reserva);
                int idReserva = reservaCreada.ID_Reserva;

                // 2. Crear el pago
                var pago = new PagoModel
                {
                    ID_Reserva = idReserva,
                    Metodo_Pago = request.Metodo_Pago ?? "Efectivo",
                    Monto = request.Monto,
                    Fecha_Pago = request.Fecha_Pago,
                    Numero_Transaccion = request.Numero_Transaccion ?? $"RES-{idReserva}-{DateTime.Now.Ticks}",
                    DetallePago = new List<DetallePagoModel>()
                };

                PagoModel pagoCreado = pagoLogica.CrearRegistro(pago);
                int idPago = pagoCreado.ID_Pago;

                // 3. Obtener datos completos para el PDF y correo
                ClienteModel cliente = clienteLogica.ObtenerPorId(request.ID_Cliente);
                PaqueteModel paquete = paqueteLogica.ObtenerPorId(request.ID_Paquete);
                DestinoModel destino = destinoLogica.ObtenerPorId(paquete.ID_Destino);

                // Construir objeto para el generador de PDF
                var pdfData = new ReservaData
                {
                    NumeroTransaccion = pagoCreado.Numero_Transaccion,
                    Cliente = new ClienteDto
                    {
                        ID_Cliente = cliente.ID_Cliente,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Telefono = cliente.Telefono,
                        Correo = cliente.Correo,
                        Pasaporte = cliente.Pasaporte,
                        Nacionalidad = cliente.Nacionalidad
                    },
                    Acompanantes = request.Acompanantes ?? new List<ClienteDto>(),
                    FechaReserva = request.Fecha_Reserva,
                    NumeroPersonas = request.Numero_Personas,
                    Paquete = request.Paquete,
                    DestinoNombre = destino.Nombre,
                    Moneda = destino.Moneda,
                    PrecioTotal = request.Precio_Total,
                    MontoPagado = request.Monto,
                    Vuelto = request.Monto - request.Precio_Total,
                    Observaciones = request.Observaciones
                };

                // 4. Generar PDF usando PuppeteerSharp
                var pdfGenerator = new PdfGeneratorService();
                byte[] pdfBytes = await pdfGenerator.GenerarComprobanteReservaAsync(pdfData);
                string pdfBase64 = Convert.ToBase64String(pdfBytes);

                // 5. Enviar correo con el PDF adjunto
                bool correoEnviado = false;
                if (!string.IsNullOrEmpty(cliente.Correo))
                {
                    try
                    {
                        await _emailService.EnviarConfirmacionReserva(
                            destinatario: cliente.Correo,
                            nombreCliente: $"{cliente.Nombre} {cliente.Apellido}",
                            nombrePaquete: paquete.Nombre,
                            destino: destino.Nombre,
                            fechaInicio: paquete.Fecha_Inicio,
                            fechaFin: paquete.Fecha_Fin,
                            precioTotal: request.Precio_Total,
                            moneda: destino.Moneda,
                            numeroPersonas: request.Numero_Personas,
                            fechaReserva: request.Fecha_Reserva,
                            numeroTransaccion: pagoCreado.Numero_Transaccion,
                            pdfAdjunto: pdfBytes
                        );
                        correoEnviado = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar correo: {ex.Message}");
                        // No detenemos el flujo, la reserva ya está creada
                    }
                }

                return Ok(new ReservaConfirmacionResponse
                {
                    Success = true,
                    ID_Reserva = idReserva,
                    ID_Pago = idPago,
                    Mensaje = "Reserva confirmada",
                    CorreoEnviado = correoEnviado,
                    PdfBase64 = pdfBase64
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, mensaje = $"Error interno: {ex.Message}" });
            }
        }
    }

    public class ReservaEstatusRequest
    {
        public int ID_Reserva { get; set; }
        public string Estatus { get; set; }
        public string CorreoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NombrePaquete { get; set; }
        public string Destino { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Moneda { get; set; }
    }
}