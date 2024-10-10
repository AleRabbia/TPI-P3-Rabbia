using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Application.Models;

public class ReservationUpdateAdmin
{
        public int? UserId { get; set; }
        public int? FieldId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public float? TotalPrice { get; set; }
        public bool? IsPaid { get;  set; }
}