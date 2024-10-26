using System;
using System.Security.Claims;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        
        [Authorize(Roles = "SysAdmin, Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetAllReservation()
        {
            try {
                var reservations = _reservationService.GetAllReservation();
                var reservationDTOs = new List<ReservationDto>();
                foreach (var reservation in reservations)
                {
                    reservationDTOs.Add(new ReservationDto
                    {
                        Id = reservation.Id,
                        UserId = reservation.UserId,
                        FieldId = reservation.FieldId,
                        DateTime = reservation.DateTime,
                        TotalPrice = reservation.TotalPrice,
                        IsPaid = reservation.IsPaid
                    });
                }
                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }


        [HttpGet("{id}")]
        public ActionResult<ReservationDto> GetReservarionById([FromRoute]int id)
        {
            try {
            var reservation = _reservationService.GetReservarionById(id);
            if (reservation == null)
            {
                return NotFound("Reserva no encontrada.");
            }
            var reservationDto = new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                FieldId = reservation.FieldId,
                DateTime = reservation.DateTime,
                TotalPrice = reservation.TotalPrice,
                IsPaid = reservation.IsPaid
            };
            return Ok(reservationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            } 
        }

        [Authorize(Roles = "SysAdmin, Admin")]
        [HttpGet("Paid")]
        public ActionResult<IEnumerable<ReservationDto>> GetReservationByPaid()
        {
            try{
            var reservations = _reservationService.GetReservationByPaid();
            if (reservations == null)
            {
                return NotFound("Reserva no encontrada.");
            }
            var reservationDtos = reservations.Select(reservation => ReservationDto.CreateReservation(reservation)).ToList();
            return Ok(reservationDtos);    
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }       

        }

        [HttpPost]
        public ActionResult<ReservationDto> AddReservation([FromBody]ReservationCreateDto reservationCreateDto)
        {
            try {
            var reservation = _reservationService.AddReservation(reservationCreateDto);
            var reservationDateTime = reservationCreateDto.Date.Add(reservationCreateDto.Time);

            var reservationDto = new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                FieldId = reservation.FieldId,
                DateTime = reservationDateTime,
                TotalPrice = reservation.TotalPrice,
                IsPaid = false
            };

            return CreatedAtAction(nameof(GetReservarionById), new { id = reservationDto.Id }, reservationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateReservation([FromRoute]int id,[FromBody] ReservationUpdateDto reservationUpdateDto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var reservation = _reservationService.GetReservarionById(id);
                if (user == reservation.UserId)
                {
                    _reservationService.UpdateReservation(id, reservationUpdateDto);
                    return NoContent();

                }
            else {
                return BadRequest($"Solo puede modificar reservas del usuario {user} ");
            }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [Authorize(Roles = "SysAdmin")]
        [HttpPatch("{id}")]
        public ActionResult UpdateReservationAdmin([FromRoute]int id, [FromBody]ReservationUpdateAdmin reservationUpdateAdmin)
        {
            try
            {
                _reservationService.UpdateReservationAdmin(id, reservationUpdateAdmin);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteReservation([FromRoute]int id)
        {
            try {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var reservation = _reservationService.GetReservarionById(id);
                if (user == reservation.UserId)
                {
                    _reservationService.DeleteReservation(id);
                    return NoContent();
                }
                else{
                    return BadRequest($"Solo puede eliminar reservas del usuario {user} ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
    
}


