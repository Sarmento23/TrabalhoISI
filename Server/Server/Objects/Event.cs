using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Objects
{
    public class Event : Base
    {
        [JsonIgnore]
        [ForeignKey("StadiumID")]
        public Stadium Stadium { get; set; }
        [Required]
        public int StadiumID { get; set; }

        [JsonIgnore]
        [ForeignKey("SectorID")]
        public Sector Sector{ get; set; }
        [Required]
        public int SectorID { get; set; }
        [Required]
        public int Percentage { get; set; }
        [JsonIgnore]
        public List<Ticket> tickets { get; set; }
        [JsonIgnore]
        public int AvailableSeats { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
