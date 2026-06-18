using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Modelos
{
    public class PaqueteTemporadaModel
    {
        [Key]
        public int ID_PaqueteTemporada { get; set; }

        public int ID_Paquete { get; set; }

        public int ID_Temporada { get; set; }

        // Navegación a Paquete
        [JsonIgnore, ForeignKey("ID_Paquete")]
        public virtual PaqueteModel? Paquete { get; set; }

        // Navegación a Temporada
        [JsonIgnore, ForeignKey("ID_Temporada")]
        public virtual TemporadaModel? Temporada { get; set; }
    }
}