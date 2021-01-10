using System;
using System.Collections.Generic;

#nullable disable

namespace Cliente.Models
{
    public partial class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int StadiumId { get; set; }
        public int SectorId { get; set; }
        public int Percentage { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Sector Sector { get; set; }
        public virtual Stadium Stadium { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
