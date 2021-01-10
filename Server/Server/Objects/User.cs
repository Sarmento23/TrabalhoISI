using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Objects
{
    public enum Role
    {
        Admin,
        Adept,
        EventOrganizer
    }
    public class User : Base
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Username só pode ter 15 caracteres")]
        public string Username { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Password só pode ter 15 caracteres")]
        public string Password { get; set; }
        [Required]
        [StringLength(9, ErrorMessage = "Contacto tem de ter 9 digitos")]
        public string Contact { get; set; }
        [Required]
        public Role Role { get; set; }

    }
}
