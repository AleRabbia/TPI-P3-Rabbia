using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Reservation
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
        public DateTime DateTime { get; set; }

        
        [Required]
        [Column(TypeName = "REAL")]
        public float TotalPrice { get; set; }

        [Required]
        public bool IsPaid { get; set; }


        public ICollection<Payment> Payments { get; set; }

        public Reservation() { }

    }
}