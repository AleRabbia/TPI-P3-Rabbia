using System;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "SysAdmin, Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<PaymentDto>> GetAllPayments()
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Payment> AddPayment([FromBody]PaymentCreateDto paymentCreateDto)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [Authorize(Roles = "SysAdmin, Admin")]
        [HttpGet("{id}")]
        public ActionResult<PaymentDto> GetPaymentById([FromRoute]int id)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("User/{UserId}")]
        public ActionResult<PaymentDto> GetPaymentByUser([FromRoute]int UserId)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePaymente([FromRoute]int id)
        {
            try {
            _paymentService.DeletePayment(id);
            return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePaymet([FromRoute]int id, [FromBody]PaymentUpdateDto paymentUpdateDto)
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