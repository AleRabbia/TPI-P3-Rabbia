using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class RegisterUserDto
    {
        public required  string Name { get; set; }
        public required  string Email { get; set; }
        public required  string Password { get; set; }
    }
}
