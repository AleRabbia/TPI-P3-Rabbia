using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class CreateFieldDto
    {
        public required  string Name { get; set; }
        public required  float Price { get; set; }
        public required  string Type { get; set; }
        public required int DurationInHours { get; set; }
    }
}
