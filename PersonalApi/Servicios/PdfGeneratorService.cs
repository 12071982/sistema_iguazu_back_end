using Modelos;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class PdfGeneratorService
    {
        public async Task<byte[]> GenerarComprobanteReservaAsync(ReservaData data)
        {
            var html = GenerarHtmlComprobante(data);

            // Descarga Chromium solo la primera vez (comentado si ya se tiene)
            // await new BrowserFetcher().DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" }
            });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);

            // Generar PDF con tamaño personalizado (80mm ancho)
            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Width = "80mm",
                PrintBackground = true,
                MarginOptions = new MarginOptions { Top = "5mm", Bottom = "5mm", Left = "5mm", Right = "5mm" },
                PreferCSSPageSize = true
            });

            return pdfBytes;
        }

        private string GenerarHtmlComprobante(ReservaData data)
        {
            var sb = new StringBuilder();

            // Estilos inline idénticos al componente del frontend
            sb.AppendLine(@"<!DOCTYPE html>");
            sb.AppendLine(@"<html><head>");
            sb.AppendLine(@"<meta charset='UTF-8'>");
            sb.AppendLine(@"<style>");
            sb.AppendLine(@"* { margin: 0; padding: 0; box-sizing: border-box; }");
            sb.AppendLine(@"body { font-family: system-ui, -apple-system, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background: white; }");
            sb.AppendLine(@".comprobante { width: 80mm; margin: 0 auto; background: #ffffff; color: #222222; font-size: 11px; padding: 12px; box-shadow: none; border-radius: 4px; }");
            sb.AppendLine(@"</style>");
            sb.AppendLine(@"</head><body>");
            sb.AppendLine($@"<div class='comprobante'>");

            // Encabezado con logo en base64 (reemplaza LOGO_EN_BASE64 por tu cadena real)
            sb.AppendLine(@"
            <div style='text-align: center; margin-bottom: 12px; padding-bottom: 12px; border-bottom: 1px dashed #aaaaaa;'>
                <img src='https://lh3.googleusercontent.com/d/1Wj7KSpPLWl7GbI0WsrMW-ayYj5wRWkJb' alt='logo' width='65px' style='margin-bottom: 6px;' />
                <p style='margin: 0 0 4px 0; font-size: 12px; font-weight: 800;'>TOUR EXPRESS IGUAZU E.I.R.L</p>
                <p style='margin: 0 0 2px 0; font-size: 8.5px;'>Agencia de Viajes y Turismo</p>
                <p style='margin: 0 0 2px 0; font-size: 8.5px;'>Huancayo, Perú</p>
                <p style='margin: 0 0 2px 0; font-size: 8.5px;'>Tel: 930 164 767</p>
                <p style='margin: 0 0 8px 0; font-size: 9px; font-weight: bold;'>RUC: 20605995480</p>
                <div style='background-color: #f8f9fa; padding: 6px; border: 1px solid #e9ecef; border-radius: 4px;'>
                    <span style='display: block; font-size: 10px; font-weight: 800;'>BOLETA ELECTRÓNICA</span>
                    <span style='display: block; font-size: 9px; margin-top: 2px; font-family: monospace;'>NRO OPERACIÓN: " + data.NumeroTransaccion + @"</span>
                </div>
            </div>");

            // Información de la reserva
            sb.AppendLine($@"
            <div style='margin-bottom: 12px; font-size: 10px; line-height: 1.4;'>
                <div style='display: flex; justify-content: space-between; margin-bottom: 3px;'>
                    <span style='color: #666666;'>Cliente:</span>
                    <span style='font-weight: 700;'>{data.Cliente.Nombre} {data.Cliente.Apellido}</span>
                </div>
                <div style='display: flex; justify-content: space-between; margin-bottom: 3px;'>
                    <span style='color: #666666;'>Teléfono:</span>
                    <span>{data.Cliente.Telefono}</span>
                </div>
                <div style='display: flex; justify-content: space-between; margin-bottom: 3px;'>
                    <span style='color: #666666;'>Fecha Reserva:</span>
                    <span>{data.FechaReserva}</span>
                </div>
                <div style='display: flex; justify-content: space-between;'>
                    <span style='color: #666666;'>Total Personas:</span>
                    <span style='font-weight: 700; background-color: #e9ecef; padding: 0 5px; border-radius: 3px;'>{data.NumeroPersonas}</span>
                </div>
            </div>");

            // Pasajeros (titular + acompañantes)
            sb.AppendLine(@"
            <div style='margin-bottom: 12px; padding-top: 8px; border-top: 1px dashed #aaaaaa;'>
                <p style='margin: 0 0 6px 0; text-align: center; font-size: 9.5px; font-weight: 800;'>PASAJEROS EN LA RESERVA</p>
                <div style='background-color: #fafafa; border: 1px solid #e9ecef; border-radius: 4px; padding: 6px; margin-bottom: 5px;'>
                    <div style='display: flex; justify-content: space-between; align-items: center; margin-bottom: 3px;'>
                        <span style='font-weight: 700;'>1. " + data.Cliente.Nombre + " " + data.Cliente.Apellido + @"</span>
                        <span style='font-size: 7.5px; font-weight: 700; color: #0066cc; background-color: #f0f7ff; padding: 1px 4px; border-radius: 3px;'>TITULAR</span>
                    </div>
                    <div style='font-size: 8.5px;'>Doc: " + (data.Cliente.Pasaporte ?? "---") + @" | Nac: " + data.Cliente.Nacionalidad + @"</div>
                </div>");

            int idx = 2;
            foreach (var a in data.Acompanantes)
            {
                sb.AppendLine($@"
                <div style='background-color: #fafafa; border: 1px solid #e9ecef; border-radius: 4px; padding: 6px; margin-bottom: 5px;'>
                    <div style='display: flex; justify-content: space-between; align-items: center; margin-bottom: 3px;'>
                        <span style='font-weight: 700;'>{idx++}. {a.Nombre} {a.Apellido}</span>
                        <span style='font-size: 7.5px; font-weight: 700; color: #288a42; background-color: #f1faf4; padding: 1px 4px; border-radius: 3px;'>ACOMP.</span>
                    </div>
                    <div style='font-size: 8.5px;'>Doc: {(a.Pasaporte ?? "---")} | Nac: {a.Nacionalidad}</div>
                </div>");
            }
            sb.AppendLine("</div>");

            // Detalles del paquete
            sb.AppendLine($@"
            <div style='margin-bottom: 12px; padding-top: 8px; border-top: 1px dashed #aaaaaa;'>
                <p style='margin: 0 0 6px 0; text-align: center; font-size: 9.5px; font-weight: 800;'>DETALLES DEL PAQUETE</p>
                <div style='background-color: #fafafa; border: 1px solid #eeeeee; border-radius: 4px; padding: 6px;'>
                    <div><strong>{data.Paquete.Nombre}</strong></div>
                    <div>Destino: {data.DestinoNombre}</div>
                    <div>Inicio: {data.Paquete.Fecha_Inicio}</div>
                    <div>Fin: {data.Paquete.Fecha_Fin}</div>
                    <div>Precio Base: {data.Paquete.Precio_Base:F2} {data.Moneda}</div>
                </div>
            </div>");

            // Observaciones (si existen)
            if (!string.IsNullOrEmpty(data.Observaciones))
                sb.AppendLine($"<div><strong>Obs:</strong> {data.Observaciones}</div>");

            // Resumen de venta
            sb.AppendLine($@"
            <div style='border: 1px solid #222222; padding: 8px; margin: 10px 0; background-color: #fafafa; border-radius: 4px;'>
                <p style='margin: 0 0 6px 0; text-align: center; font-size: 9.5px; font-weight: 800;'>RESUMEN DE VENTA</p>
                <div style='display: flex; justify-content: space-between;'>Multi-Pasajero: {data.NumeroPersonas} × {data.Paquete.Precio_Base:F2}</div>
                <div style='font-weight: 800; border-bottom: 1px dashed #cccccc; margin-bottom: 6px;'>TOTAL IMPORTE: {data.PrecioTotal:F2} {data.Moneda}</div>
                <div>Monto Pagado: {data.MontoPagado:F2} {data.Moneda}</div>
                <div style='border-top: 1px solid #dddddd; margin-top: 2px;'>Vuelto: {data.Vuelto:F2} {data.Moneda}</div>
            </div>");

            // Políticas de cancelación (solamente la frase indicada)
            sb.AppendLine($@"
            <div style='text-align: center; margin-top: 12px; padding-top: 8px; border-top: 1px dashed #aaaaaa; font-size: 8.5px;'>
                <p>¡Gracias por su preferencia!</p>
                <p><strong>Política de cancelación:</strong> Toda cancelación debe comunicarse a la agencia dentro de las 24 horas posteriores a la reserva. Se aplicará una penalidad del 10% sobre el monto total pagado.</p>
                <p>Emitida el: {data.FechaReserva}</p>
            </div>");

            sb.AppendLine("</div></body></html>");
            return sb.ToString();
        }
    }

    public class ReservaData
    {
        public string NumeroTransaccion { get; set; }
        public ClienteDto Cliente { get; set; }
        public List<ClienteDto> Acompanantes { get; set; }
        public string FechaReserva { get; set; }
        public int NumeroPersonas { get; set; }
        public PaqueteDto Paquete { get; set; }
        public string DestinoNombre { get; set; }
        public string Moneda { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Vuelto { get; set; }
        public string Observaciones { get; set; }
    }
}