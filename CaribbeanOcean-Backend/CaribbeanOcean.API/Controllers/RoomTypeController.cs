using System.Text.Json;
using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.API.Services;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.RoomType;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomTypeController(
        RoomTypeService roomTypeService,
        CloudinaryService cloudinaryService,
        ActivityLogService logService
        ) : ControllerBase
    {
        // GET: api/RoomType
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomTypes = await roomTypeService.GetAll();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.RoomTypeView,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(roomTypes);
        }

        // GET: api/RoomType/PageClient
        [HttpGet("PageClient")]
        public async Task<IActionResult> GetRoomTypePage()
        {
            var roomTypes = await roomTypeService.GetRoomTypPage();
            return Ok(roomTypes);
        }

        // POST: api/RoomType
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
            [FromForm] string name,
            [FromForm] string? description,
            [FromForm] decimal dailyRate,
            [FromForm] string? features,
            IFormFile? image)
        {
            string? imageUrl = null;

            if (image != null)
                imageUrl = await cloudinaryService.UploadImageAsync(image);

            var dto = new CreateRoomTypeDTO
            {
                Name = name,
                Description = description,
                DailyRate = dailyRate,
                ImageUrl = imageUrl,
                Features = features != null
                    ? JsonSerializer.Deserialize<IEnumerable<string>>(features)
                    : null
            };

            var generatedId = await roomTypeService.Create(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.RoomTypeCreate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Room type '{name}' created (id: {generatedId})"
                );

            return Created(string.Empty, new { Id = generatedId });
        }

        // PUT: api/RoomType/{id}
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] string name,
            [FromForm] string? description,
            [FromForm] decimal dailyRate,
            [FromForm] string? features,
            IFormFile? image)
        {
            string? imageUrl = null;

            if (image != null)
                imageUrl = await cloudinaryService.UploadImageAsync(image);

            var dto = new UpdateRoomTypeDTO
            {
                Name = name,
                Description = description,
                DailyRate = dailyRate,
                ImageUrl = imageUrl,
                Features = features != null
                    ? JsonSerializer.Deserialize<IEnumerable<string>>(features)
                    : null
            };

            await roomTypeService.Update(id, dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.RoomTypeUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Room type #{id} updated ('{name}')"
                );

            return NoContent();
        }

        // DELETE: api/RoomType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await roomTypeService.Delete(id);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.RoomTypeDelete,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Room type #{id} deleted"
                );

            return NoContent();
        }

        // GET: api/RoomType/Available
        [HttpGet("Available")]
        public async Task<IActionResult> GetAllAvailable([FromBody] RoomTypeRequestDTO dto)
        {
            var roomTypes = await roomTypeService.GetAllAvailable(dto);
            return Ok(roomTypes);
        }

        [HttpPost("AvailableCount")]
        public async Task<IActionResult> GetAllAvailableCount([FromBody] RoomTypeRequestDTO dto)
        {
            var roomTypes = await roomTypeService.GetAllAvailableCount(dto);
            return Ok(roomTypes);
        }

        // POST: api/RoomType/Available/{id}
        [HttpPost("Available/{id}")]
        public async Task<IActionResult> GetAvailableById([FromBody] RoomTypeRequestDTO dto)
        {
            var roomType = await roomTypeService.GetAvailableById(dto);
            return Ok(roomType);
        }
    }
}