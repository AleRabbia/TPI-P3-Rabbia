using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class UpdateUserDto
    {
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Dirección de correo incorrecta")]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }

}
