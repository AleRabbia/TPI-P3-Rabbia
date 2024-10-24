using System;

namespace Application.Models
{
    public class CreateReviewDto
    {
        public int UserId { get; set; }
        public int FieldId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}

