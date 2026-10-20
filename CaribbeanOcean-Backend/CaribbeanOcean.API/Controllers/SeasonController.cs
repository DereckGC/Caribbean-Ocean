using System.Threading.Tasks;
using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Season;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers;

// Sets the base URL for this controller to: https://localhost:{{port}}/api/Season
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SeasonController(SeasonService seasonService, ActivityLogService logService) : ControllerBase
{
    // GET: api/Season
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var seasons = await seasonService.GetAll();

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value, 
                ActionCodes.SeasonView, 
                ClaimsHelper.GetScreen(HttpContext), 
                ClaimsHelper.GetIpAddress(HttpContext));

        return Ok(seasons);
    }

    // GET: api/Season/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var season = await seasonService.GetById(id);

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value,
                ActionCodes.SeasonView,
                ClaimsHelper.GetScreen(HttpContext),
                ClaimsHelper.GetIpAddress(HttpContext),
                $"Season ID searched: {id}"
            );

        return Ok(season);
    }

    // POST: api/Season

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateSeasonDTO dto)
    {
        try
        {
            // Crear season
            var generatedId = await seasonService.Create(dto);

            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.SeasonCreate,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"Season created (id: {generatedId})"
                );

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
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSeasonDTO dto)
    {
        await seasonService.Update(id, dto);

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value,
                ActionCodes.SeasonUpdate,
                ClaimsHelper.GetScreen(HttpContext),
                ClaimsHelper.GetIpAddress(HttpContext),
                $"Season #{id} updated"
            );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await seasonService.Delete(id);

        var userId = ClaimsHelper.GetUserId(HttpContext);
        if (userId.HasValue)
            await logService.LogAsync(
                userId.Value,
                ActionCodes.SeasonDelete,
                ClaimsHelper.GetScreen(HttpContext),
                ClaimsHelper.GetIpAddress(HttpContext),
                $"Season #{id} deleted"
            );

        return Ok(new
        {
            message = "Season deleted successfully"
        });
    }
}
