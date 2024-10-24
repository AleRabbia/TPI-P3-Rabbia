using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Application.Models;

public class UpdateuserDtoAdmin
{
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Dirección de correo incorrecta")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool? Enabled {get; set;}
}
