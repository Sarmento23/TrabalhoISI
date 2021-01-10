using System;
using System.Collections.Generic;

#nullable disable

namespace Cliente.Models
{
    public partial class Sector
    {
        public Sector()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Colunas { get; set; }
        public int Linhas { get; set; }
        public int StadiumId { get; set; }
        public int Capacidade { get; set; }
        public virtual Stadium Stadium { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
