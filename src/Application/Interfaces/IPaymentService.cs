using System;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
            IEnumerable<Payment> GetAllPayment();
            Payment GetPaymentById(int id);
            IEnumerable<Payment> GetPaymentByUser(int userId);
            Payment AddPayment(PaymentCreateDto paymentCreateDto);
            void UpdatePayment(int id, PaymentUpdateDto paymentUpdateDto);
            void DeletePayment(int id);
    }
}