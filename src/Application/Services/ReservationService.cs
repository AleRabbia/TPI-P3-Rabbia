using System;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReservationService : IReservationService
    {
        
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, IFieldRepository fieldRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }
        public Reservation AddReservation(ReservationCreateDto reservationCreateDto)
        {
            var user = _userRepository.GetById(reservationCreateDto.UserId);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            var field = _fieldRepository.GetFieldById(reservationCreateDto.FieldId);
            if (field == null)
                throw new Exception("Campo no encontrado");

            DateTime reservationDateTime = reservationCreateDto.Date.Date.Add(reservationCreateDto.Time);
            DateTime endTime = reservationDateTime.AddHours(field.DurationInHours);

            var reservations = _reservationRepository.GetAllReservation().
            Where(r => r.FieldId == reservationCreateDto.FieldId &&
                    r.DateTime < endTime && 
                    r.DateTime.AddHours(field.DurationInHours) > reservationDateTime);
            if (reservations.Any())
            {
                throw new Exception($"La Cancha {reservationCreateDto.FieldId} no esta disponible para la fecha solicitada");
            }

            var reservation = new Reservation
            {
                UserId = reservationCreateDto.UserId,
                FieldId = reservationCreateDto.FieldId,
                DateTime = reservationDateTime,
                TotalPrice = field.Price,
                IsPaid = false
            };

            _reservationRepository.AddReservation(reservation);
            return reservation;
        }

        public void DeleteReservation(int id)
        {
            _reservationRepository.DeleteReservation(id);
        }

        public IEnumerable<Reservation> GetAllReservation()
        {
            return _reservationRepository.GetAllReservation();
        }

        public Reservation GetReservarionById(int id)
        {
            return _reservationRepository.GetReservarionById(id);
        }

        public IEnumerable<Reservation> GetReservationByPaid()
        {
            return _reservationRepository.GetReservationByPaid();
        }

        public void UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto)
        {
            var reservation = _reservationRepository.GetReservarionById(id);
            if (reservation == null)
                throw new Exception("Reserva no encontrada.");

            var field = _fieldRepository.GetFieldById(reservationUpdateDto.FieldId);
            if (field == null)
                throw new Exception("El campo no existe.");

            DateTime reservationDateTime = reservationUpdateDto.Date.Date.Add(reservationUpdateDto.Time);
            DateTime endTime = reservationDateTime.AddHours(field.DurationInHours);

            var reservations = _reservationRepository.GetAllReservation().
            Where(r => r.FieldId == reservationUpdateDto.FieldId &&
                    r.DateTime < endTime && 
                    r.DateTime.AddHours(field.DurationInHours) > reservationDateTime);
            if (reservations.Any())
            {
                throw new Exception($"La Cancha {reservationUpdateDto.FieldId} no esta disponible para la fecha solicitada");
            }


            reservation.FieldId = reservationUpdateDto.FieldId;
            reservation.DateTime = reservationDateTime;

            _reservationRepository.UpdateReservation(reservation);
            
        }

        public void UpdateReservationAdmin(int id, ReservationUpdateAdmin reservationUpdateAdmin)
        {
            //Field field = null;

            bool hasDateTimeChange = false;

            var reservationExisting = _reservationRepository.GetReservarionById(id);
            if (reservationExisting == null)
                throw new Exception("Reserva no encontrada.");

                if ((reservationUpdateAdmin.Date.HasValue || reservationUpdateAdmin.Time.HasValue) 
        && !reservationUpdateAdmin.FieldId.HasValue)
                {
                    throw new Exception("FieldId es obligatorio cuando se actualiza la fecha o la hora.");
                }
            
            if (reservationUpdateAdmin.FieldId.HasValue)
            {                
                var field = _fieldRepository.GetFieldById(reservationUpdateAdmin.FieldId.Value);
                if (field == null)
                    throw new Exception("El campo no existe.");
                reservationExisting.FieldId = reservationUpdateAdmin.FieldId.Value;

                var currentDate = reservationExisting.DateTime.Date;  // Solo la fecha
            var currentTime = reservationExisting.DateTime.TimeOfDay;  // Solo la hora

            DateTime newDateTime;

// Validar y actualizar la fecha si se proporciona en el DTO y es distinta a la fecha existente
            if (reservationUpdateAdmin.Date.HasValue && reservationUpdateAdmin.Date != currentDate)
            {
                currentDate = reservationUpdateAdmin.Date.Value.Date;  // Actualizar la fecha si es necesario
                hasDateTimeChange = true;
            }

// Validar y actualizar la hora si se proporciona en el DTO y distinta a la hora exist
            if (reservationUpdateAdmin.Time.HasValue && reservationUpdateAdmin.Time != currentTime)
            {
                currentTime = reservationUpdateAdmin.Time.Value;
                hasDateTimeChange = true;
            }

// Combinar la nueva fecha y hora
            if (hasDateTimeChange)
            {
                newDateTime = currentDate.Add(currentTime);

                DateTime reservationDateTime = newDateTime;//reservationUpdateAdmin.Date.Value.Date.Add(reservationUpdateAdmin.Time.Value);
                DateTime endTime = reservationDateTime.AddHours(reservationExisting.Field.DurationInHours);

                var reservations = _reservationRepository.GetAllReservation().
                Where(r => r.FieldId == reservationUpdateAdmin.FieldId &&
                        r.DateTime < endTime && 
                        r.DateTime.AddHours(reservationExisting.Field.DurationInHours) > reservationDateTime);
                if (reservations.Any())
                {
                    throw new Exception($"La Cancha {reservationUpdateAdmin.FieldId} no esta disponible para la fecha solicitada");
                }
                reservationExisting.DateTime = reservationDateTime;
            }


            }
            
            if (reservationUpdateAdmin.UserId.HasValue)
            {
                var user = _userRepository.GetById(reservationUpdateAdmin.UserId.Value);
                if (user == null)
                    throw new Exception("El usuario no existe");
                reservationExisting.UserId = reservationUpdateAdmin.UserId.Value;
            }
            if (reservationUpdateAdmin.TotalPrice != null)
            {
                reservationExisting.TotalPrice = reservationUpdateAdmin.TotalPrice.Value;
                Console.WriteLine($"Total {reservationUpdateAdmin.TotalPrice.Value}");
            }
            if (reservationUpdateAdmin.IsPaid != null)
            {
                reservationExisting.IsPaid = reservationUpdateAdmin.IsPaid.Value;
                Console.WriteLine($"IsPaid {reservationUpdateAdmin.IsPaid.Value}");
            }


            _reservationRepository.UpdateReservationAdmin(reservationExisting);
        }
    }
}