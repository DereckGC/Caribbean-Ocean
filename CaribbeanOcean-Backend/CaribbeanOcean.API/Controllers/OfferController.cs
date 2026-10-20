using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    // Sets the base URL for this controller to: https://localhost:{{port}}/api/Offer
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfferController (OfferService service, ActivityLogService logService) : Controller
    {
        // GET: api/Offer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await service.GetAll();
                return Ok(response); // Returns HTTP 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/admin/offers
        [HttpGet("admin/offers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await service.GetAdminOffers();
                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value, 
                        ActionCodes.OfferView, 
                        ClaimsHelper.GetScreen(HttpContext), 
                        ClaimsHelper.GetIpAddress(HttpContext));

                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/admin/offers
        [HttpDelete("admin/offers/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await service.Delete(id);

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.OfferDelete,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Offer #{id} deleted"
                    );

                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/admin/offers
        [HttpPost("admin/offers")]
        public async Task<IActionResult> Create([FromBody] OfferAdminDTO offer)
        {
            try
            {
                if (offer == null)
                {
                    return BadRequest(new { message = "Invalid Offer Data" });
                }

                var response = await service.Create(offer);

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.OfferCreate,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Offer created: {offer.Name}"
                    );

                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/admin/offers
        [HttpPut("admin/offers")]
        public async Task<IActionResult> Update([FromBody] OfferAdminDTO offer)
        {
            try
            {

                if (offer == null)
                {
                    return BadRequest(new { message = "Invalid Offer Data" });
                }

                var response = await service.Update(offer);
                if (!response)
                {
                    return BadRequest(new { message = "Error Updating Offer" });
                }

                var userId = ClaimsHelper.GetUserId(HttpContext);
                if (userId.HasValue)
                    await logService.LogAsync(
                        userId.Value,
                        ActionCodes.OfferUpdate,
                        ClaimsHelper.GetScreen(HttpContext),
                        ClaimsHelper.GetIpAddress(HttpContext),
                        $"Offer #{offer.Id} updated"
                    );

                return Ok(response); // Returns HTTP 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
