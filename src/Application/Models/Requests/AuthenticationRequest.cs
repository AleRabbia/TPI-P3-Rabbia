using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models.Requests
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
