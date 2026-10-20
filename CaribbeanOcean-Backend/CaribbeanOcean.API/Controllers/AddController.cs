using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Add;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Authorize]
    public class AddController(AddService service, ActivityLogService logService) : Controller
    {
        // GET: api/admin/adds
        [HttpGet("api/admin/adds")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await service.GetAll();

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value, ActionCodes.AddView, 
                        ClaimsHelper.GetScreen(HttpContext), 
                        ClaimsHelper.GetIpAddress(HttpContext)
                        );


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/admin/adds?adminId=1
        [HttpPost("api/admin/adds")]
        public async Task<IActionResult> Create(
            [FromForm] CreateAddRequestDTO request,
            IFormFile image,
            [FromQuery] int adminId)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Invalid Add Data" });

                var response = await service.Create(image, request.TargetURL, adminId);

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.AddCreate,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Advertisement created (target: {request.TargetURL})"
                    );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/admin/adds/{id}?adminId=1
        [HttpPut("api/admin/adds/{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateAddRequestDTO request,
            IFormFile? newImage,
            [FromQuery] int adminId)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Invalid Add Data" });

                var response = await service.Update(id, newImage, request, adminId);
                if (!response)
                    return BadRequest(new { message = "Error Updating Add" });

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.AddUpdate,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Advertisement #{id} updated"
                    );


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/admin/adds/{id}
        [HttpDelete("api/admin/adds/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await service.Delete(id);

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.AddDelete,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Advertisement #{id} deleted"
                    );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}