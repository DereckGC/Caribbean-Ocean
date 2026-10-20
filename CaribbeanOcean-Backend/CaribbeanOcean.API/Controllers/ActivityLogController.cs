using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController(ActivityLogService logService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? id,
            [FromQuery] string? username,
            [FromQuery] int? actionCode,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo)
        {
            try
            {
                var result = await logService.GetAllAsync(id, username, actionCode, dateFrom, dateTo);
                return Ok(result);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}