using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Objects
{
    public class Sector : Base
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Nome do setor só pode ter 5 caracteres")]
        public string Name { get; set; }
        [Required]
        public int Colunas { get; set; }
        [Required]
        public int Linhas { get; set; }
        
        [JsonIgnore]
        public int Capacidade { get; set;}

        [JsonIgnore]
        [ForeignKey("StadiumID")]
        public Stadium Stadium { get; set; }
        [Required]
        public int StadiumID { get; set; }

    }
}
