using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public required  string Name { get; set; }
        public required  string Email { get; set; }
        public required string Role { get; set; }
        public bool Enabled {get; set;}

        public static UserDto Create(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Enabled = user.Enabled
            };
        }
    }

}
