using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Modelos
{
    public class TemporadaModel
    {
        [Key]
        public int ID_Temporada { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal PrecioBase { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<PaqueteTemporadaModel> PaqueteTemporadas { get; set; } = new List<PaqueteTemporadaModel>();
    }
}