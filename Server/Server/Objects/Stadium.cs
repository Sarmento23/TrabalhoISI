using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Objects
{
    public class Stadium : Base
    {

        public Stadium()
        {
            Sectors = new List<Sector>();
            tickets = new List<Ticket>();
            Events = new List<Event>();
        }

        [Required]
        [MaxLength(20, ErrorMessage = "Nome do estádio só pode ter 20 caracteres")]
        public string Name { get; set; }
        //[JsonIgnore]
        public List<Sector> Sectors { get; set; }

        [JsonIgnore]
        public int Capacidade { get; set; }

        [JsonIgnore]
        public List<Ticket> tickets { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
