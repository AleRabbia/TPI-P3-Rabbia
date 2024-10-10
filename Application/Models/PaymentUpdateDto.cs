using System;

namespace Application.Models
{
    public class PaymentUpdateDto
    {
        public required int ReservationId { get; set; }
        public required float Amount { get; set; }
        public required string Method { get; set; }
    }
}
