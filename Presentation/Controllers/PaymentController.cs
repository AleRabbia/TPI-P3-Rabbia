using System;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {        
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaymentDto>> GetAllPayments()
        {
            var payments = _paymentService.GetAllPayment();
            var paymentDTOs = new List<PaymentDto>();
            foreach (var payment in payments)
            {
                paymentDTOs.Add(new PaymentDto
                {
                    Id = payment.Id,
                    ReservationId = payment.ReservationId,
                    Amount = payment.Amount,
                    Method = payment.Method
                });
            }
            return Ok(paymentDTOs);
        }

        [HttpPost]
        public ActionResult<Payment> AddPayment(PaymentCreateDto paymentCreateDto)
        {

            var payment = _paymentService.AddPayment(paymentCreateDto);
            
            var paymentDto = new PaymentDto
            {
                Id = payment.Id,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                Method = payment.Method
            };

            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, paymentDto);
        }
        
        [HttpGet("{id}")]
        public ActionResult<PaymentDto> GetPaymentById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound("Pago no encontrada.");
            }
            var paymentDto = new PaymentDto
            {
                Id = payment.Id,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                Method = payment.Method
            };
            return Ok(paymentDto);
        }

        [HttpGet("User/{UserId}")]
        public ActionResult<PaymentDto> GetPaymentByUser(int UserId)
        {
            var paymentsByUser = _paymentService.GetPaymentByUser(UserId);
            if (paymentsByUser == null)
            {
                return NotFound($"El usuario {UserId} no posee pagos realizados.");
            }

            var paymentDTOs = new List<PaymentDto>();
            foreach (var payment in paymentsByUser)
            {
                paymentDTOs.Add(new PaymentDto
                {
                    Id = payment.Id,
                    ReservationId = payment.ReservationId,
                    Amount = payment.Amount,
                    Method = payment.Method
                });
            }
            return Ok(paymentDTOs);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePaymente(int id)
        {
            _paymentService.DeletePayment(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePaymet(int id, PaymentUpdateDto paymentUpdateDto)
        {
            try
            {
                _paymentService.UpdatePayment(id, paymentUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}