using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Application.Models
{
    public class UpdateFieldDtoAdmin
    {
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? Type { get; set; }
        public int? DurationInHours { get; set; }
        public bool? Enabled {get; set;}

    }
}