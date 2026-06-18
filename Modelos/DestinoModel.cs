using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{

    public class DestinoModel
    {
        [Key] // es la llave primaria de mi base de datos
      
        public int ID_Destino { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Atracciones { get; set; }
        public string Clima { get; set; }
        public string Idioma { get; set; }
        public string Moneda { get; set; }
    }
}