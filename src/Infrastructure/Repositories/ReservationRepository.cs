using System;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Reservation> GetAllReservation()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservarionById(int id)
        {
            return _context.Reservations.Find(id);
        }

        public IEnumerable<Reservation> GetReservationByPaid()
        {
            return _context.Reservations.Where(r => r.IsPaid).ToList();
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }
        
        public void UpdateReservationAdmin(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }
    }
}


