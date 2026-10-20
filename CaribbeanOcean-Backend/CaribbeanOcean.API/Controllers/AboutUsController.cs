using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.AboutUs;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AboutUsController(AboutUsService aboutUsService, ActivityLogService logService) : ControllerBase
    {
        // GET: api/AboutUs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutUs = await aboutUsService.Get();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.AboutView,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext)
            );

            return Ok(aboutUs);
        }

        // PUT: api/AboutUs
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdminAboutUsDTO dto)
        {
            await aboutUsService.Update(dto);

            //Crear registro en bitácora
            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.AboutUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext)
            );

            return NoContent();
        }
    }
}