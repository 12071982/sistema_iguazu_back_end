using System.Collections.Generic;

namespace Modelos
{
    public class ReservaConfirmacionRequest
    {
        public int ID_Cliente { get; set; }
        public int ID_Paquete { get; set; }
        public string Fecha_Reserva { get; set; }
        public int Numero_Personas { get; set; }
        public decimal Precio_Total { get; set; }
        public string Observaciones { get; set; }
        public string Metodo_Pago { get; set; }
        public decimal Monto { get; set; }
        public string Fecha_Pago { get; set; }
        public string Numero_Transaccion { get; set; }
        public ClienteDto Cliente { get; set; }
        // Lista de acompañantes
        public List<ClienteDto> Acompanantes { get; set; }
        public PaqueteDto Paquete { get; set; }
        public string Moneda { get; set; }
        public string DestinoNombre { get; set; }
    }

    public class ClienteDto
    {
        public int ID_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Pasaporte { get; set; }
        public string Nacionalidad { get; set; }
    }

    public class PaqueteDto
    {
        public int ID_Paquete { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Fin { get; set; }
        public decimal Precio_Base { get; set; }
        public int ID_Destino { get; set; }
        public string Inclusiones { get; set; }
        public string Exclusiones { get; set; }
    }

    public class ReservaConfirmacionResponse
    {
        public bool Success { get; set; }
        public int ID_Reserva { get; set; }
        public int ID_Pago { get; set; }
        public string Mensaje { get; set; }
        public bool CorreoEnviado { get; set; }
        public string PdfBase64 { get; set; }
    }
}