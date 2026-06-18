using System.Net;
using System.Net.Mail;

namespace Servicios
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void EnviarCancelacion(
            string destinatario,
            string nombreCliente,
            string nombrePaquete,
            string destino,
            string fechaInicio,
            string fechaFin,
            decimal precioTotal,
            string moneda,
            int numeroPersonas
        )
        {
            decimal penalidad = precioTotal * 0.10m;
            decimal devolucion = precioTotal - penalidad;

            string cuerpo = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin:0; padding:0; background-color:#f0f4f8; font-family: Arial, sans-serif;'>

  <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f0f4f8; padding: 40px 0;'>
    <tr>
      <td align='center'>
        <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:12px; overflow:hidden; box-shadow: 0 4px 20px rgba(0,0,0,0.1);'>

          <!-- HEADER -->
          <tr>
            <td style='background: linear-gradient(135deg, #0f2027, #203a43, #2c5364); padding: 35px 40px; text-align: center;'>
              <img
                src='https://lh3.googleusercontent.com/a/ACg8ocIGnD2kIID32lwkhlSEC4hqXImU2B50zOzVo7QTqCe9Rf0dQQo=s100-c'
                alt='Tours Express'
                width='80'
                height='80'
                style='border-radius: 50%; border: 3px solid rgba(255,255,255,0.3); margin-bottom: 15px; display: block; margin-left: auto; margin-right: auto;'
              />
              <h1 style='color:#ffffff; margin:0; font-size:26px; letter-spacing:2px;'>TOURS EXPRESS</h1>
              <p style='color:rgba(255,255,255,0.6); margin: 6px 0 0; font-size:13px; letter-spacing:1px;'>IGUAZÚ</p>
            </td>
          </tr>

          <!-- BANDA ROJA DE ALERTA -->
          <tr>
            <td style='background-color:#c0392b; padding: 12px 40px; text-align:center;'>
              <p style='margin:0; color:#ffffff; font-size:14px; font-weight:bold; letter-spacing:1px;'>
                ⚠️ NOTIFICACIÓN DE CANCELACIÓN DE RESERVA
              </p>
            </td>
          </tr>

          <!-- SALUDO -->
          <tr>
            <td style='padding: 35px 40px 10px;'>
              <p style='font-size:16px; color:#2c3e50; margin:0 0 10px;'>Estimado/a <strong>{nombreCliente}</strong>,</p>
              <p style='font-size:14px; color:#555; line-height:1.7; margin:0;'>
                Le comunicamos que su reserva ha sido <strong style='color:#c0392b;'>cancelada</strong> en nuestro sistema.
                A continuación encontrará el detalle completo de su reserva y las condiciones aplicables según nuestras políticas.
              </p>
            </td>
          </tr>

          <!-- DETALLE DE RESERVA -->
          <tr>
            <td style='padding: 25px 40px 10px;'>
              <p style='font-size:13px; font-weight:bold; color:#2c5364; text-transform:uppercase; letter-spacing:1px; border-bottom: 2px solid #2c5364; padding-bottom:8px; margin:0 0 15px;'>
                📄 Detalle de la Reserva
              </p>
              <table width='100%' cellpadding='0' cellspacing='0' style='border-collapse:collapse; font-size:14px;'>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; width:40%; color:#555;'><strong>Paquete</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50;'>{nombrePaquete}</td>
                </tr>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; color:#555;'><strong>Destino</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50;'>📍 {destino}</td>
                </tr>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; color:#555;'><strong>Fecha de inicio</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50;'>📅 {fechaInicio}</td>
                </tr>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; color:#555;'><strong>Fecha de fin</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50;'>📅 {fechaFin}</td>
                </tr>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; color:#555;'><strong>N° de personas</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50;'>👥 {numeroPersonas}</td>
                </tr>
                <tr>
                  <td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6; color:#555;'><strong>Precio total</strong></td>
                  <td style='padding:10px 14px; background:#ffffff; border:1px solid #dee2e6; color:#2c3e50; font-weight:bold;'>{precioTotal:F2} {moneda}</td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- POLÍTICA DE CANCELACIÓN -->
          <tr>
            <td style='padding: 20px 40px 10px;'>
              <table width='100%' cellpadding='0' cellspacing='0' style='background: linear-gradient(135deg, #fff8e1, #fff3cd); border-left: 5px solid #f39c12; border-radius: 6px;'>
                <tr>
                  <td style='padding: 20px 22px;'>
                    <p style='margin:0 0 10px; font-size:14px; font-weight:bold; color:#856404;'>📋 Política de Cancelación</p>
                    <p style='margin:0 0 8px; font-size:13px; color:#666; line-height:1.6;'>
                      De acuerdo con nuestras políticas, se aplicará una <strong>penalidad del 10%</strong> sobre el monto total de la reserva.
                    </p>
                    <table width='100%' cellpadding='0' cellspacing='0' style='margin-top:12px;'>
                      <tr>
                        <td style='font-size:13px; color:#555; padding: 4px 0;'>Monto original:</td>
                        <td style='font-size:13px; color:#555; text-align:right; padding: 4px 0;'>{precioTotal:F2} {moneda}</td>
                      </tr>
                      <tr>
                        <td style='font-size:13px; color:#c0392b; padding: 4px 0;'>Penalidad (10%):</td>
                        <td style='font-size:13px; color:#c0392b; text-align:right; padding: 4px 0;'>- {penalidad:F2} {moneda}</td>
                      </tr>
                      <tr>
                        <td colspan='2' style='border-top: 1px solid #e0c060; padding-top:8px; margin-top:8px;'></td>
                      </tr>
                      <tr>
                        <td style='font-size:15px; font-weight:bold; color:#155724; padding: 4px 0;'>Monto a devolver:</td>
                        <td style='font-size:15px; font-weight:bold; color:#155724; text-align:right; padding: 4px 0;'>{devolucion:F2} {moneda}</td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- COORDINACIÓN DE DEVOLUCIÓN -->
          <tr>
            <td style='padding: 15px 40px 10px;'>
              <table width='100%' cellpadding='0' cellspacing='0' style='background: linear-gradient(135deg, #e8f4fd, #d1ecf1); border-left: 5px solid #17a2b8; border-radius: 6px;'>
                <tr>
                  <td style='padding: 20px 22px;'>
                    <p style='margin:0 0 8px; font-size:14px; font-weight:bold; color:#0c5460;'>📞 Coordinación de Devolución</p>
                    <p style='margin:0; font-size:13px; color:#555; line-height:1.7;'>
                      La solicitud de devolución será gestionada <strong>únicamente a través de nuestra agencia</strong>.
                      Le pedimos que se comunique con nosotros para coordinar el proceso de reembolso.
                      <strong>No realizamos devoluciones automáticas.</strong>
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- CIERRE -->
          <tr>
            <td style='padding: 25px 40px 30px;'>
              <p style='font-size:14px; color:#555; line-height:1.7; margin:0 0 16px;'>
                Lamentamos los inconvenientes que esta situación pueda ocasionarle y quedamos a su disposición para cualquier consulta adicional.
              </p>
              <p style='font-size:14px; color:#2c3e50; margin:0;'>
                Atentamente,<br>
                <strong>El equipo de Tours Express Iguazú</strong>
              </p>
            </td>
          </tr>

          <!-- FOOTER -->
          <tr>
            <td style='background: linear-gradient(135deg, #0f2027, #203a43, #2c5364); padding: 20px 40px; text-align:center;'>
              <p style='margin:0 0 6px; color:rgba(255,255,255,0.9); font-size:13px; font-weight:bold;'>Tours Express Iguazú</p>
              <p style='margin:0; color:rgba(255,255,255,0.5); font-size:11px;'>
                Este es un correo automático, por favor no responda directamente a este mensaje.
              </p>
            </td>
          </tr>

        </table>
      </td>
    </tr>
  </table>

</body>
</html>";

            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var displayName = _config["EmailSettings:DisplayName"];

            var mensaje = new MailMessage
            {
                From = new MailAddress(from, displayName),
                Subject = "🔴 Cancelación de Reserva — Tours Express Iguazú",
                Body = cuerpo,
                IsBodyHtml = true
            };
            mensaje.To.Add(destinatario);

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            smtp.Send(mensaje);
        }

        public async Task EnviarConfirmacionReserva(
            string destinatario,
            string nombreCliente,
            string nombrePaquete,
            string destino,
            string fechaInicio,
            string fechaFin,
            decimal precioTotal,
            string moneda,
            int numeroPersonas,
            string fechaReserva,
            string numeroTransaccion,
            byte[] pdfAdjunto,
            string nombrePdf = "comprobante_reserva.pdf"
        )
        {
            // Cuerpo del correo (incluye políticas y datos de empresa)
            string cuerpo = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin:0; padding:0; background-color:#f0f4f8; font-family: Arial, sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f0f4f8; padding: 40px 0;'>
    <tr>
      <td align='center'>
        <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:12px; overflow:hidden; box-shadow: 0 4px 20px rgba(0,0,0,0.1);'>
          <!-- HEADER (igual que antes) -->
          <tr>
            <td style='background: linear-gradient(135deg, #0f2027, #203a43, #2c5364); padding: 35px 40px; text-align: center;'>
              <img src='https://lh3.googleusercontent.com/a/ACg8ocIGnD2kIID32lwkhlSEC4hqXImU2B50zOzVo7QTqCe9Rf0dQQo=s100-c' alt='Tours Express' width='80' height='80' style='border-radius: 50%; border: 3px solid rgba(255,255,255,0.3); margin-bottom: 15px; display: block; margin-left: auto; margin-right: auto;' />
              <h1 style='color:#ffffff; margin:0; font-size:26px; letter-spacing:2px;'>TOURS EXPRESS</h1>
              <p style='color:rgba(255,255,255,0.6); margin: 6px 0 0; font-size:13px; letter-spacing:1px;'>IGUAZÚ</p>
            </td>
          </tr>
          <!-- BANDA VERDE -->
          <tr>
            <td style='background-color:#27ae60; padding: 12px 40px; text-align:center;'>
              <p style='margin:0; color:#ffffff; font-size:14px; font-weight:bold; letter-spacing:1px;'>✓ CONFIRMACIÓN DE RESERVA</p>
            </td>
          </tr>
          <!-- SALUDO -->
          <tr>
            <td style='padding: 35px 40px 10px;'>
              <p style='font-size:16px; color:#2c3e50; margin:0 0 10px;'>Estimado/a <strong>{nombreCliente}</strong>,</p>
              <p style='font-size:14px; color:#555; line-height:1.7; margin:0;'>¡Gracias por elegir <strong>Tours Express Iguazú</strong>! Su reserva ha sido <strong style='color:#27ae60;'>confirmada exitosamente</strong>. Adjunto encontrará su comprobante en formato PDF.</p>
            </td>
          </tr>
          <!-- DETALLE DE RESERVA (tabla resumida) -->
          <tr><td style='padding: 25px 40px 10px;'>
            <p style='font-size:13px; font-weight:bold; color:#2c5364; border-bottom:2px solid #2c5364; padding-bottom:8px;'>📄 Detalle de la Reserva</p>
            <table width='100%' cellpadding='0' cellspacing='0' style='border-collapse:collapse; font-size:14px;'>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>N° Transacción</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>{numeroTransaccion}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Fecha reserva</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>📅 {fechaReserva}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Paquete</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>{nombrePaquete}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Destino</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>📍 {destino}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Fecha inicio</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>📅 {fechaInicio}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Fecha fin</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>📅 {fechaFin}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>N° personas</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6;'>👥 {numeroPersonas}</td></tr>
              <tr><td style='padding:10px 14px; background:#f8f9fa; border:1px solid #dee2e6;'><strong>Total pagado</strong></td><td style='padding:10px 14px; border:1px solid #dee2e6; font-weight:bold;'>{precioTotal:F2} {moneda}</td></tr>
            </table>
           </td></tr>

            <!-- POLÍTICAS Y DATOS DE EMPRESA -->
          <tr>
            <td style='padding: 20px 40px;'>
              <table width='100%' cellpadding='0' cellspacing='0' style='background: #fff8e1; border-left: 5px solid #f39c12; border-radius: 6px;'>
                <tr><td style='padding: 20px 22px;'>
                  <p style='margin:0 0 10px; font-size:14px; font-weight:bold; color:#856404;'>📋 Política de Cancelación</p>
                  <p style='margin:0 0 8px; font-size:13px; color:#555;'>Toda cancelación debe comunicarse a la agencia dentro de las 24 horas posteriores a la reserva. Se aplicará una penalidad del 10% sobre el monto total pagado.</p>
                  <hr style='margin: 12px 0;'>
                  <p style='margin:0; font-size:13px; color:#2c3e50;'><strong>🏢 Tours Express Iguazú E.I.R.L</strong><br>
                  📍 Dirección: Av. San Martín 1234, Puerto Iguazú, Misiones, Argentina<br>
                  📞 Teléfono: +54 3757 123456 / +54 9 3757 654321<br>
                  ✉️ Email: reservas@toursexpressiguazu.com<br>
                  🕒 Horario: Lun-Dom 8:00 a 20:00 hs<br>
                  🔗 Web: www.toursexpressiguazu.com</p>
                </td></tr>
              </table>
            </td>
          </tr>

          <!-- FOOTER -->
          <tr><td style='background: linear-gradient(135deg, #0f2027, #203a43, #2c5364); padding: 20px 40px; text-align:center;'>
            <p style='margin:0 0 6px; color:rgba(255,255,255,0.9); font-size:13px; font-weight:bold;'>Tours Express Iguazú</p>
            <p style='margin:0; color:rgba(255,255,255,0.5); font-size:11px;'>Este es un correo automático, por favor no responda directamente a este mensaje.</p>
          </td></tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";

            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var displayName = _config["EmailSettings:DisplayName"];

            using (var mensaje = new MailMessage())
            {
                mensaje.From = new MailAddress(from, displayName);
                mensaje.To.Add(destinatario);
                mensaje.Subject = "✅ Confirmación de Reserva - Tours Express Iguazú";
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = true;

                if (pdfAdjunto != null && pdfAdjunto.Length > 0)
                {
                    using (var stream = new MemoryStream(pdfAdjunto))
                    {
                        var attachment = new Attachment(stream, nombrePdf, "application/pdf");
                        mensaje.Attachments.Add(attachment);

                        using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(from, password);
                            smtp.EnableSsl = true;
                            smtp.Timeout = 10000; // 10 segundos
                            smtp.UseDefaultCredentials = false;
                            await smtp.SendMailAsync(mensaje);
                        }
                    }
                }
                else
                {
                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(from, password);
                        smtp.EnableSsl = true;
                        smtp.Timeout = 10000;
                        smtp.UseDefaultCredentials = false;
                        await smtp.SendMailAsync(mensaje);
                    }
                }
            }
        
    }
    }
}