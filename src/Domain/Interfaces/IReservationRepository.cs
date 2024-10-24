using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAllReservation();
        Reservation GetReservarionById(int id);
        IEnumerable<Reservation> GetReservationByPaid();
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        
        void UpdateReservationAdmin(Reservation reservation);
        void DeleteReservation(int id);
    }
}

