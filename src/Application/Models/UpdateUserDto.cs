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
        public required string Password { get; set; }
    }

}
