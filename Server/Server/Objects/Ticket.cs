using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Objects
{
    public class Ticket : Base
    {
        [Required]
        public string AdeptName { get; set; }
        [Required]
        public int AdeptContact { get; set; }

        [JsonIgnore]
        [ForeignKey("StadiumID")]
        public Stadium Stadium { get; set; }
        [Required]
        public int StadiumID { get; set; }

        [JsonIgnore]
        [ForeignKey("EventID")]
        public Event Event { get; set; }
        [Required]
        public int EventID { get; set; }

        [Required]
        public string SectorName { get; set; }
        [Required]
        public int Local { get; set; }
    }
}