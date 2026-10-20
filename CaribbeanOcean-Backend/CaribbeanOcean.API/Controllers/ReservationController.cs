using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Pricing;
using CaribbeanOcean.Application.DTOs.Reservation;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Application.UseCases;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController(ReservationService reservationService, ReservationPricingService reservationPricingService, IReservationNightDetailRepository reservationNightDetailRepository, ActivityLogService logService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await reservationService.GetAll();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.ReservationView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await reservationService.GetById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.ReservationView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext), 
                    $"Reservation ID searched:{id}"
                );


            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDTO dto)
        {
            var generatedCode = await reservationService.Create(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.ReservationCreate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Reservation #{generatedCode} created"
                );

            return Created(string.Empty, new { code = generatedCode });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReservationDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { message = "Route id does not match dto id." });
            }

            var UpdatetReservation = await reservationService.GetById(id);

            bool updated = await reservationService.Update(dto);
            
            if (!updated)
            {
                return NotFound(new { success = false, message = "Error updating the Reservation." });
            }

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.ReservationUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Reservation #{UpdatetReservation.Code} updated, Client: {dto.ClientName} {dto.ClientLastname}"
                );

            return Ok(new { success = true });
        }

        [HttpPost("HasAvailableRoom")]
        public async Task<IActionResult> HasAvailableRoom([FromBody] RoomTypeRequestDTO dto)
        {
            bool hasAvailableRoom = await reservationService.HasAvailableRoom(dto);

            return Ok(new { hasAvailableRoom });
        }

        [HttpPost("availability-update")]
        public async Task<IActionResult> CheckAvailabilityForUpdate(
        [FromBody] UpdateAvailabilityRequestDTO request)
        {
            var isAvailable = await reservationService.HasAvailableRoomForUpdate(
                request
            );

            return Ok(new
            {
                hasAvailableRoom = isAvailable
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await reservationService.GetById(id);
            bool deletedReservation = await reservationService.Delete(id);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.ReservationDelete,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Reservation #{reservation.Code} deleted, Room #{reservation.RoomId} available"
                );

            return Ok(deletedReservation);
        }

        [HttpPost("price-preview")]
        public async Task<IActionResult> PreviewPrice( [FromBody] CalculateReservationPriceRequestDTO request)
        {
            var result = await reservationPricingService.Calculate(request);
            return Ok(result);
        }

        [HttpGet("{id:int}/night-details")]
        public async Task<IActionResult> GetReservationNightDetails(int id)
        {
            var details = await reservationNightDetailRepository.GetActiveByReservationId(id);

            return Ok(details);
        }
    }
}