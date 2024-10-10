using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Nombre no puede tener mas de 100 caracteres")]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "REAL")]
        public float Price { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Type { get; set; }
        [Required]
        public int DurationInHours { get; set; }

        [Required]
        public bool Enabled { get; set; }

        // Navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public Field(){ }
    }
}