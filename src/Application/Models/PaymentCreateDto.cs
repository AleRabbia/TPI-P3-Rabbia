using System;

namespace Application.Models
{
    public class PaymentCreateDto
    {
        public required int ReservationId { get; set; }
        public required string Method { get; set; }
    }
}