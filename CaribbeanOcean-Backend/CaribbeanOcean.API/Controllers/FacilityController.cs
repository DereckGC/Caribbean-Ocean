using System.Text.Json;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.API.Services;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Application.DTOs.Facility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CaribbeanOcean.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController(FacilityService facilityService, CloudinaryService cloudinaryService, ActivityLogService logService) : Controller
    {
        

        // GET: api/Facility
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var location = await facilityService.GetFacilitiesPage();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.FacilityView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(location); // Returns HTTP 200 OK
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFacilities()
        {
            var facilities = await facilityService.GetFacilityDTOs();

            return Ok(facilities);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFacilityById(int id)
        {
            var facility = await facilityService.GetFacilityById(id);

            return Ok(facility);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacility(
            [FromForm] string nameFacility,
            [FromForm] string? description,
            [FromForm] string? featuresJson,
            [FromForm] int displayOrder,
            [FromForm] int updatedBy,
            [FromForm] bool active,
            [FromForm] IFormFile? image)
        {
            string? imageUrl = null;

            if (image != null)
                imageUrl = await cloudinaryService.UploadImageAsync(image);

            var features = JsonSerializer.Deserialize<List<string>>(featuresJson ?? "[]") ?? new();

            var dto = new CreateFacilityDTO
            {
                Name = nameFacility,
                Description = description,
                Features = features,
                ImageUrl = imageUrl,
                DisplayOrder = displayOrder,
                UpdatedBy = updatedBy,
                Active = active
            };
            var createdId = await facilityService.CreateFacility(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.FacilityCreate,
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Facility created (id: {createdId})");

            return CreatedAtAction(
                nameof(GetFacilityById),
                new { id = createdId },
                new { id = createdId }
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFacility(int id,
            [FromForm] string nameFacility,
            [FromForm] string? description,
            [FromForm] string? featuresJson,
            [FromForm] int displayOrder,
            [FromForm] bool active,
            [FromForm] int updatedBy,
            [FromForm] string? imageUrl,
            [FromForm] IFormFile? image)
        {
            string? finalImageUrl = imageUrl;

            if (image != null)
            {
                finalImageUrl = await cloudinaryService.UploadImageAsync(image);
            }

            var features = JsonSerializer.Deserialize<List<string>>(featuresJson ?? "[]") ?? new();

            var dto = new UpdateFacilityDTO
            {
                Id = id,
                Name = nameFacility,
                Description = description,
                Features = features,
                ImageUrl = finalImageUrl,
                DisplayOrder = displayOrder,
                updated_by = updatedBy,
                Active = active
            };

            var updated = await facilityService.UpdateFacility(id, dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.FacilityUpdate,
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Facility #{id} updated");

            return Ok(new{ updated });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFacility(int id, [FromQuery] int? updatedBy = null)
        {
            var deleted = await facilityService.DeleteFacility(id, updatedBy);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.FacilityDelete,
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Facility #{id} deleted");

            return Ok(new { deleted });
        }
    }
}
