using System;
using System.Collections.Generic;

#nullable disable

namespace Cliente.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public string AdeptName { get; set; }
        public int AdeptContact { get; set; }
        public int StadiumId { get; set; }
        public int EventId { get; set; }
        public string SectorName { get; set; }
        public int Local { get; set; }

        public virtual Event Event { get; set; }
        public virtual Stadium Stadium { get; set; }
    }
}
