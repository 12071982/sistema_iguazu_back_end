using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modelos
{
    public class DetallePagoModel
    {
        [Key] // es la llave primaria de mi base de datos
        public int ID_Detalle_Pago { get; set; }
        public int ID_Pago { get; set; }
        public decimal Cuota { get; set; }                       
        public string Fecha_Vencimiento { get; set; }
        public string Estatus { get; set; }

        [JsonIgnore, ForeignKey("ID_Pago")]
        public virtual PagoModel? Pago { get; set; }
    }
}
