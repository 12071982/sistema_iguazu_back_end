using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{


    [Table("error")]
    public class ErrorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public int id_user { get; set; }
        public string url { get; set; }
        [MaxLength(100)]
        public string controller { get; set; }
        [MaxLength(20)]
        public string ip { get; set; }
        [MaxLength(20)]
        public string method { get; set; }
        [MaxLength(150)]
        public string user_agent { get; set; }
        public string host { get; set; }
        [MaxLength(50)]
        public string class_component { get; set; }
        [MaxLength(50)]
        public string function_name { get; set; }
        public int line_number { get; set; }
        public string error { get; set; }
        public string StackTrace { get; set; }
        public bool status { get; set; }
        public string request { get; set; }
        public int error_code { get; set; }
        public DateTime dateCreate { get; set; }
    }
}
