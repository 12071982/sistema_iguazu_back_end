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
   
    public class ReservaModel
    {
        [Key] // es la llave primaria de mi base de datos        
        public int ID_Reserva { get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Paquete { get; set; }
        public string Fecha_Reserva { get; set; }
        public int Numero_Personas { get; set; }
        public decimal Precio_Total { get; set; }
        public string Estatus { get; set; }
        public string Observaciones { get; set; }

        // Navegación hacia Cliente
        [ForeignKey("ID_Cliente")]
        public virtual ClienteModel Cliente { get; set; }

        // Navegación hacia Pago (uno a uno)
        public virtual PagoModel Pago { get; set; }
    }
}
