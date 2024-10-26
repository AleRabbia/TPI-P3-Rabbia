using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class CreateUserDto
    {
        public required  string Name { get; set; }
        [EmailAddress]
        public required  string Email { get; set; }
        public required  string Password { get; set; }
        public required string Role { get; set; }
    }
}
