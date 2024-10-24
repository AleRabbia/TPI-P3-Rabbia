using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int FieldId { get; set; }

        
        [ForeignKey("FieldId")]
        public Field Field { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "La puntuación debe ser entre 0 y 5.")]
        public int Rating { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Comment { get; set; }

        public Review() { }
    }
}