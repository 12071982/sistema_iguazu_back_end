using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class PagoModel
    {
        [Key] // es la llave primaria de mi base de dato
        public int ID_Pago { get; set; }
        [Required]
        public int ID_Reserva { get; set; }
        public string Metodo_Pago { get; set; }
        public decimal Monto { get; set; }
        public string Fecha_Pago { get; set; }
        public string Numero_Transaccion { get; set; }

        // Navegación hacia Reserva
        [ForeignKey("ID_Reserva")]
        public virtual ReservaModel Reserva { get; set; }
        public virtual List<DetallePagoModel> DetallePago { get; set; }
    }
}
