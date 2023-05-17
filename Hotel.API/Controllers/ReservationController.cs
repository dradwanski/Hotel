using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using System.Security.Claims;
using Hotel.Database.Entities;

namespace Hotel.API.Controllers
{
    [Route("Reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public ReservationController(IMapper mapper, IReservationService reservationService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
        }


        [HttpPost]
        public async Task<ActionResult> CreateReservationAsync([FromBody] ReservationModel reservationModel)
        {
            if(!DateOnly.TryParse(reservationModel.ReservationStart, out var startDate))
            {
                return BadRequest();
            }
            reservationModel.ReservationStart = startDate.ToString();

            if (!DateOnly.TryParse(reservationModel.ReservationEnd, out var endDate))
            {
                return BadRequest();
            }
            reservationModel.ReservationEnd = endDate.ToString();

            var reservationDto = _mapper.Map<ReservationDto>(reservationModel);

            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            reservationDto.CreatedUserId = userId;
            reservationDto.LastEditedUserId = userId;


            await _reservationService.CreateReservationAsync(reservationDto);
            return Ok();
        }

        [HttpPut("MethodOfPayment/{reservationId}")]
        public async Task<ActionResult> PutMethodOfPaymentAsync([FromRoute] int reservationId, [FromQuery] int methodOfPaymentId)
        {
            await _reservationService.PutMethodOfPaymentAsync(reservationId, methodOfPaymentId);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteReservationAsync([FromRoute] int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }

        [HttpGet("GetBy")]
        public async Task<ActionResult> GetReservationAsync([FromQuery]int? Clientid, [FromQuery]int? RoomId, [FromQuery]string? startDate, [FromQuery]string? endDate, [FromQuery]int? pageSize = 10, [FromQuery]int? pageNumber = 1)
        {
            if (Clientid is not null)
            {
                var reservations = await _reservationService.GetReservationsByClientidAsync(Clientid.Value, pageSize.Value, pageNumber.Value);
                return Ok(reservations);
            }
            if (RoomId is not null)
            {
                var reservations = await _reservationService.GetReservationsByRoomIdAsync(RoomId.Value, pageSize.Value, pageNumber.Value);
                return Ok(reservations);
            }
            if (startDate is not null && endDate is not null)
            {
                if (!DateOnly.TryParse(startDate, out var startReservationDate))
                {
                    return BadRequest();
                }
                startDate = startReservationDate.ToString();

                if (!DateOnly.TryParse(endDate, out var endReservationDate))
                {
                    return BadRequest();
                }
                endDate = endReservationDate.ToString();

                var reservations = await _reservationService.GetReservationsByDateAsync(startDate, endDate, pageSize.Value, pageNumber.Value);
                return Ok(reservations);
            }

            return NotFound();
        }


        [HttpPut("/{reservationId}")]
        public async Task<ActionResult> UpdateReservationAsync([FromRoute] int reservationId, [FromBody] ReservationModel reservationModel)
        {
            if (!DateOnly.TryParse(reservationModel.ReservationStart, out var startDate))
            {
                return BadRequest();
            }
            reservationModel.ReservationStart = startDate.ToString();

            if (!DateOnly.TryParse(reservationModel.ReservationEnd, out var endDate))
            {
                return BadRequest();
            }
            reservationModel.ReservationEnd = endDate.ToString();
            var reservationDto = _mapper.Map<ReservationDto>(reservationModel);

            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            reservationDto.LastEditedUserId = userId;
            reservationDto.ReservationId = reservationId;

            await _reservationService.UpdateReservationAsync(reservationId, reservationDto);
            return Ok();
        }

        [HttpPut("Confrim/{reservationId}")]
        public async Task<ActionResult> ConfirmReservationAsync([FromRoute] int reservationId)
        {
            await _reservationService.ConfirmReservationAsync(reservationId);
            return Ok();
        }

        [HttpPut("Cancel/{reservationId}")]
        public async Task<ActionResult> CancelReservationAsync([FromRoute] int reservationId)
        {
            await _reservationService.CancelReservationAsync(reservationId);
            return Ok();
        }
    }
}
