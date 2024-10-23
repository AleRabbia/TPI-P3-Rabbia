using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class UpdateFieldDto
    {
        public required string Name { get; set; }
        public float Price { get; set; }
        public required string Type { get; set; }
    }
}