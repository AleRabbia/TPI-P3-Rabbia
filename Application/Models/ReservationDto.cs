using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Application.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FieldId { get; set; }
        public DateTime DateTime { get; set; }
        public float TotalPrice { get; set; }
        public bool IsPaid { get;  set; }
        public static ReservationDto CreateReservation(Reservation reservation)
        {
            return new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                FieldId = reservation.FieldId,
                DateTime = reservation.DateTime,
                TotalPrice =reservation.TotalPrice,
                IsPaid = reservation.IsPaid
            };
        }
    }


}