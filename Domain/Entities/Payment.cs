using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }
        
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [Required]
        [Column(TypeName = "REAL")]        
        public float Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Method { get; set; }

        public Payment() { }
    }
}