using System;
using System.Security.Claims;
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
        private readonly IReservationService _reservationService;

        public PaymentController(IPaymentService paymentService, IReservationService reservationService)
        {
            _paymentService = paymentService;
            _reservationService = reservationService;
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

        [Authorize]
        [HttpGet("User/{UserId}")]
        public ActionResult<PaymentDto> GetPaymentByUser([FromRoute]int UserId)
        {
            try {
            var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (user == UserId)
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
            else {
                return BadRequest($"Solo puede ver los pagos del usuario {user} ");
            }
            
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeletePaymente([FromRoute]int id)
        {
            try {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var pay = _paymentService.GetPaymentById(id);
                var reservation = _reservationService.GetReservarionById(pay.ReservationId);
            if (user == reservation.UserId)
            {
            _paymentService.DeletePayment(id);
            return NoContent();
            }
            else{
                return BadRequest($"Solo puede eliminar pagos del usuario {user} ");
            }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdatePaymet([FromRoute]int id, [FromBody]PaymentUpdateDto paymentUpdateDto)
        {
            try
            {   
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var pay = _paymentService.GetPaymentById(id);
                var reservation = _reservationService.GetReservarionById(pay.ReservationId);
            if (user == reservation.UserId)
            {
                _paymentService.UpdatePayment(id, paymentUpdateDto);
                return NoContent();
            }
            else
            {
                return BadRequest($"Solo puede modificar pagos del usuario {user} ");
            }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}