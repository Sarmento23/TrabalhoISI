using System;
using System.Collections.Generic;

#nullable disable

namespace Cliente.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public int Role { get; set; }
    }
}
