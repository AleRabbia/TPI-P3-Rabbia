using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Nombre no puede tener mas de 100 caracteres")]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Dirección de correo incorrecta")]
        [Column(TypeName = "NVARCHAR(100)")]  
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Column(TypeName = "NVARCHAR(100)")]   
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool Enabled { get; set; }


        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public User() { }
    }
}