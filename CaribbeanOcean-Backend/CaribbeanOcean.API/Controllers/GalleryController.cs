using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Gallery;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Application.UseCases.Gallery;
using CaribbeanOcean.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CaribbeanOcean.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GalleryController(
        GalleryService galleryService,
        GetGallery getGallery,
        AddGalleryImage addGalleryImage,
        UpdateGalleryImage updateGalleryImage,
        DeleteGalleryImage deleteGalleryImage,
        ActivityLogService logService) : Controller
    {
        // GET: api/Gallery
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var location = await galleryService.Get();

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.GalleryView, 
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext));

            return Ok(location);
        }

        // POST api/gallery?adminId=1
        [HttpPost]
        public async Task<IActionResult> Add(
            [FromForm] AddGalleryImageRequestDTO request,
            IFormFile image,
            [FromQuery] int adminId)
        {
            await addGalleryImage.ExecuteAsync(image, request.Alt, adminId);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.GalleryCreate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Gallery image added (alt: {request.Alt})"
                );

            return Ok(new { message = "Image added successfully." });
        }

        // PUT api/gallery/{imageId}?adminId=1
        [HttpPut("{imageId:int}")]
        public async Task<IActionResult> Update(
            int imageId,
            [FromForm] UpdateGalleryImageRequestDTO request,
            IFormFile newImage,
            [FromQuery] int adminId)
        {
            await updateGalleryImage.ExecuteAsync(imageId, newImage, request.Alt, adminId);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.GalleryUpdate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Gallery image #{imageId} updated"
                );
            return Ok(new { message = "Image updated successfully." });
        }

        // DELETE api/gallery/{imageId}?adminId=1
        [HttpDelete("{imageId:int}")]
        public async Task<IActionResult> Delete(int imageId, [FromQuery] int adminId)
        {
            try
            {
                await deleteGalleryImage.ExecuteAsync(imageId, adminId);

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.GalleryDelete,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Gallery image #{imageId} deleted"
                    );

                return Ok(new { message = "Image deleted successfully." });
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }

}