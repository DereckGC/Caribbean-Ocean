using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.API.Services;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Hero;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HeroController(
        HeroService heroService,
        CloudinaryService cloudinaryService,
        ActivityLogService logService
        ) : ControllerBase
    {
        // GET: api/Hero
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hero = await heroService.Get();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.HeroView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    "User reviewed Hero");


            return Ok(hero);
        }

        // GET: api/Hero/images
        [HttpGet("images")]
        public async Task<IActionResult> GetImages()
        {
            var images = await heroService.GetImages();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.HeroView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    "User reviewed Hero iamges");

            return Ok(images);
        }

        // PUT: api/Hero/text
        [HttpPut("text")]
        public async Task<IActionResult> UpdateText([FromBody] UpdateHeroTextDTO dto)
        {
            await heroService.UpdateText(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.HeroTextUpdate, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext)
                    );

            return NoContent();
        }

        // POST: api/Hero/images
        [HttpPost("images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImage([FromForm] string alt, IFormFile image)
        {
            var imageUrl = await cloudinaryService.UploadImageAsync(image, "hero");

            var dto = new AddHeroImageDTO
            {
                Alt = alt,
                ImageUrl = imageUrl
            };

            await heroService.AddImage(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.HeroImageCreate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Hero image added (alt: {alt})"
                );

            return NoContent();
        }

        // PUT: api/Hero/images/{imageId}
        [HttpPut("images/{imageId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] string alt, IFormFile? image)
        {
            string? imageUrl = null;

            if (image != null)
                imageUrl = await cloudinaryService.UploadImageAsync(image, "hero");

            var dto = new UpdateHeroImageDTO
            {
                Alt = alt,
                ImageUrl = imageUrl
            };

            await heroService.UpdateImage(imageId, dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.HeroImageUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Hero image #{imageId} updated"
                );

            return NoContent();
        }

        // DELETE: api/Hero/images/{imageId}
        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            await heroService.DeleteImage(imageId);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.HeroImageDelete,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Hero image #{imageId} deleted"
                );

            return NoContent();
        }
    }
}