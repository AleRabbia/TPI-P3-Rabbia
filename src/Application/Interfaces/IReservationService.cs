using System;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservation();
        Reservation GetReservarionById(int id);
        IEnumerable<Reservation> GetReservationByPaid();
        Reservation AddReservation(ReservationCreateDto reservationCreateDto);
        void UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto);
        void UpdateReservationAdmin(int id, ReservationUpdateAdmin reservationUpdateAdmin);
        void DeleteReservation(int id);
    }
}