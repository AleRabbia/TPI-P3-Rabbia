using System;

namespace Application.Models
{
    public class UpdateReviewDto
    {
        public int FieldId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
