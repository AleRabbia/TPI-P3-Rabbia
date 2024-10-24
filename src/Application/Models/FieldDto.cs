using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class FieldDto
    {
        public int Id { get; set; }
        public required  string Name { get; set; }
        public required  float Price { get; set; }
        public required string Type { get; set; }
        public required int DurationInHours { get; set; }
        public bool Enabled {get; set;}

        public static FieldDto CreateField(Field field)
        {
            return new FieldDto
            {
                Id = field.Id,
                Name = field.Name,
                Price = field.Price,
                Type = field.Type,
                DurationInHours = field.DurationInHours,
                Enabled = field.Enabled
            };
        }
    }

}
