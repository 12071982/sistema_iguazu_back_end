using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Modelos
{
    public class PaqueteModel
    {
        [Key]
        public int ID_Paquete { get; set; }
        public int ID_Destino { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Duracion { get; set; }
        public decimal Precio_Base { get; set; }
        public string Tipo { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Fin { get; set; }
        public string Inclusiones { get; set; }
        public string Exclusiones { get; set; }
        public string? Imagen { get; set; }

        [JsonIgnore]
        public virtual ICollection<PaqueteTemporadaModel> PaqueteTemporadas { get; set; }
        public PaqueteModel()
        {
            PaqueteTemporadas = new List<PaqueteTemporadaModel>();
        }
    }
}