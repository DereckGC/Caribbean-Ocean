using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.HotelContact;
using CaribbeanOcean.Application.DTOs.Season;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers;

// Sets the base URL for this controller to: https://localhost:{{port}}/api/HotelContact
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HotelContactController(HotelContactService hotelContactService, ActivityLogService logService) : ControllerBase
{
    // GET: api/HotelContact
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotelContacts = await hotelContactService.GetAll();

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value, 
                ActionCodes.HotelContactView,
                ClaimsHelper.GetScreen(HttpContext), 
                ClaimsHelper.GetIpAddress(HttpContext));

        return Ok(hotelContacts); // Returns HTTP 200 OK
    }

    // GET: api/HotelContact/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hotelContact = await hotelContactService.GetById(id);       
        return Ok(hotelContact);
    }

    // POST: api/HotelContact       

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] HotelContactDTO dto)
    {
        try
        {
            // Crear hotel contact
            var generatedId = await hotelContactService.Create(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value, 
                    ActionCodes.HotelContactCreate,
                    ClaimsHelper.GetScreen(HttpContext), 
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Hotel contact created (id: {generatedId})");


            // Success
            return Created(string.Empty, new
            {
                Id = generatedId
            });
        }
        catch (Exception ex)
        {
            // Error del SP o backend
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] HotelContactDTO dto)
    {
        await hotelContactService.Update(id, dto);

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value,
                ActionCodes.HotelContactUpdate,
                ClaimsHelper.GetScreen(HttpContext), 
                ClaimsHelper.GetIpAddress(HttpContext),
                $"Hotel contact #{id} updated");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await hotelContactService.Delete(id);

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value, 
                ActionCodes.HotelContactDelete,
                ClaimsHelper.GetScreen(HttpContext), 
                ClaimsHelper.GetIpAddress(HttpContext),
                $"Hotel contact #{id} deleted");

        return Ok(new
        {
            message = "Hotel contact deleted successfully"
        });
    }
}
