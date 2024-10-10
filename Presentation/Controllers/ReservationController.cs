using System;
using Application.Interfaces;
using Application.Models;
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
        
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetAllReservation()
        {
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


        [HttpGet("{id}")]
        public ActionResult<ReservationDto> GetReservarionById(int id)
        {
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


        [HttpGet("Paid")]
        public ActionResult<IEnumerable<ReservationDto>> GetReservationByPaid()
        {
            var reservations = _reservationService.GetReservationByPaid();
            if (reservations == null)
            {
                return NotFound("Reserva no encontrada.");
            }
            var reservationDtos = reservations.Select(reservation => ReservationDto.CreateReservation(reservation)).ToList();
            return Ok(reservationDtos);           

        }

        [HttpPost]
        public ActionResult<ReservationDto> AddReservation(ReservationCreateDto reservationCreateDto)
        {

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

        [HttpPut("{id}")]
        public ActionResult UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto)
        {
            try
            {
                _reservationService.UpdateReservation(id, reservationUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPatch("{id}")]
        public ActionResult UpdateReservationAdmin(int id, ReservationUpdateAdmin reservationUpdateAdmin)
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
        

        [HttpDelete("{id}")]
        public ActionResult DeleteReservation(int id)
        {

            _reservationService.DeleteReservation(id);
            return NoContent();
        }


    }
    
}


