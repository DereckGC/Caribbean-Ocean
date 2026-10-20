using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController(RoomService roomService, ActivityLogService logService) : ControllerBase
    {
        // GET: api/Room
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await roomService.GetAll();

                //Crear registro en bitácora
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value, ActionCodes.RoomView, 
                        ClaimsHelper.GetScreen(HttpContext), 
                        ClaimsHelper.GetIpAddress(HttpContext));

                return Ok(result);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // GET: api/Room/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await roomService.GetById(id);
                if (result == null) return NotFound($"Room with id {id} not found.");

                //Crear registro en bitácora
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.RoomView,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Room ID searched:{id}"
                    );

                return Ok(result);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // POST: api/Room
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDTO dto)
        {
            try
            {
                var newId = await roomService.Create(dto);

                //Crear registro en bitácora
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.RoomCreate,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Room #{dto.Number} created"
                    );

                return Ok(new { id = newId, message = "Room created successfully." });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // PUT: api/Room/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomDTO dto)
        {
            try
            {
                dto.Id = id;
                var result = await roomService.Update(dto);
                if (!result) return NotFound($"Room with id {id} not found.");

                //Crear registro en bitácora
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value, 
                        ActionCodes.RoomUpdate, 
                        ClaimsHelper.GetScreen(HttpContext), 
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Room #{dto.Number} updated (active: {dto.Active})" 
                );

                return Ok(new { message = "Room updated successfully." });
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // DELETE: api/Room/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var room = await roomService.GetById(id);
                var result = await roomService.Delete(id);
                if (!result) return NotFound($"Room with id {id} not found.");

                //Crear registro en bitácora
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.RoomDelete,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        room != null ? $"Room #{room.Number} deleted" : $"Room id {id} deleted" 
                    );

                return Ok(new { message = "Room deleted successfully." });
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // GET: api/Room/status-today
        [HttpGet("status-today")]
        public async Task<IActionResult> GetRoomStatusToday([FromQuery] int? roomTypeId)
        {
            var result = await roomService.GetRoomStatusToday(roomTypeId);

            var response = RoomMapper.ToAvailableRoomDTOList(result);

            //Crear registro en bitácora
            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, ActionCodes.RoomStatus, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(response);
        }
    }
}