﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Cliente.Models
{
    public partial class Stadium
    {
        public Stadium()
        {
            Events = new HashSet<Event>();
            Sectors = new HashSet<Sector>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Sector> Sectors { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
