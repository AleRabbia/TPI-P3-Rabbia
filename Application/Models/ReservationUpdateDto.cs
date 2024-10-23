using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class ReservationUpdateDto
    {
        public int FieldId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }

}
