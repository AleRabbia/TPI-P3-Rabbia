using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayment();
        Payment GetPaymentById(int id);
        IEnumerable<Payment> GetPaymentByUser(int userId);
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int id);
    }
}

