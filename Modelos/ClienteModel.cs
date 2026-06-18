using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
   
    public class ClienteModel
    {

        [Key] // es la llave primaria de mi base de datos        
        public int ID_Cliente { get; set; }
        public int ID_Usuario {  get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Pasaporte { get; set; }
        public string Frecuencia_Viajero { get; set; }

        public virtual ICollection<ReservaModel> Reservas { get; set; }
    }
}
