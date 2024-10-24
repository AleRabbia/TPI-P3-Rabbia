using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FieldId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

}
