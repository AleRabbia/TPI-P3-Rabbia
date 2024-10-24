using System;
using Domain.Entities;

namespace Application.Models
{
    public class PaymentDto
    {
        public required int Id { get; set; }
        public required int ReservationId { get; set; }
        public required float Amount { get; set; }
        public required string Method { get; set; }
        public static PaymentDto CreatePayment(Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                Method = payment.Method
            };
        }
    }
}
