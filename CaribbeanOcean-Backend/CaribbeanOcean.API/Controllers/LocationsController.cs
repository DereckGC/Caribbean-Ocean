using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.DTOs.Location;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LocationsController (LocationService locationsService, ActivityLogService logService) : Controller
    {

        // GET: api/Location
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var location = await locationsService.Get();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.LocationView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(location); // Returns HTTP 200 OK
        }

        // PUT: api/Location
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdminLocationsDTO dto)
        {
            if (dto == null)
                return BadRequest("Location data is required.");
            Console.WriteLine("Entra a controller con ", dto);
            await locationsService.Update(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.LocationUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext));

            return NoContent();
        }

    }
}
