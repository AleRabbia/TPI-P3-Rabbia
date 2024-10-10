using System;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }

        }

        public IEnumerable<Payment> GetAllPayment()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public IEnumerable<Payment> GetPaymentByUser(int userId)
        {
          
                
            return _context.Payments
            .Include(p => p.Reservation)         // Incluir la Reservation
            .ThenInclude(r => r.User)            // Incluir el User asociado a la Reservation
            .Where(p => p.Reservation.UserId == userId)  // Filtrar por UserId en la Reservation
            .ToList();
            
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }
    }
}