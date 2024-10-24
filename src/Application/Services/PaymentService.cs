using System;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;
        public PaymentService(IPaymentRepository paymentRepository,IReservationRepository reservationRepository, IUserRepository userRepository, IFieldRepository fieldRepository)
        {
            _paymentRepository = paymentRepository;
             _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }

        public Payment AddPayment(PaymentCreateDto paymentCreateDto)
        {
            var reservation = _reservationRepository.GetReservarionById(paymentCreateDto.ReservationId);
            if (reservation == null)
                throw new Exception("Reserva no encontrada");
            if (reservation.IsPaid == true)
                throw new Exception("La reserva ya se encuentra pagada.");
            var payment = new Payment
            {
                ReservationId = paymentCreateDto.ReservationId,
                Amount = reservation.TotalPrice,
                Method = paymentCreateDto.Method
            };
            _paymentRepository.AddPayment(payment);
           
            reservation.IsPaid = true;
            _reservationRepository.UpdateReservation(reservation);
            return payment;
        }

        public void DeletePayment(int id)
        {   
            var payment = _paymentRepository.GetPaymentById(id);
            if (payment == null)
                throw new Exception("Pago no encontrado");

            var reservation = _reservationRepository.GetReservarionById(payment.ReservationId);
            if (reservation == null)
                throw new Exception("Reserva no encontrada");

            _paymentRepository.DeletePayment(id);

            reservation.IsPaid = false;
            _reservationRepository.UpdateReservation(reservation);
        }

        public IEnumerable<Payment> GetAllPayment()
        {
            return _paymentRepository.GetAllPayment();
        }

        public Payment GetPaymentById(int id)
        {
            return _paymentRepository.GetPaymentById(id);
        }

        public IEnumerable<Payment> GetPaymentByUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("Usuario no encontrado.");
                
            return _paymentRepository.GetPaymentByUser(userId);
        }

        public void UpdatePayment(int id, PaymentUpdateDto paymentUpdateDto)
        {
            
            var paymentExisting = _paymentRepository.GetPaymentById(id);
            if (paymentExisting == null)
                throw new Exception ("Pago no encontrado");

            var reservations = _reservationRepository.GetReservarionById(paymentUpdateDto.ReservationId);
            if (reservations == null)
                throw new Exception("Reserva no encontrada");
            
            paymentExisting.ReservationId = paymentUpdateDto.ReservationId;
            paymentExisting.Amount = paymentUpdateDto.Amount;
            paymentExisting.Method = paymentUpdateDto.Method;

            _paymentRepository.UpdatePayment(paymentExisting);
        }
    }
}


