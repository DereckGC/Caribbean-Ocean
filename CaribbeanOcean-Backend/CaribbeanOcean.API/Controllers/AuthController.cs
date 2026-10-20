using CaribbeanOcean.API.Helpers;
using CaribbeanOcean.Application.Constants;
using CaribbeanOcean.Application.DTOs.Admin;
using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthService _service, ActivityLogService logService) : Controller
    {
        // POST /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
        {
            try
            {
                var id = await _service.Register(dto);

                await logService.LogAsync(
                    id,
                    ActionCodes.Register,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext),
                    $"User {dto.Username} registered"
                );

                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
            
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var result = await _service.Login(req.Username, req.Password);

            if (result is null)
                return Unauthorized(new { message = "Invalid credentials." });

            // Registrar login en bitácora
            await logService.LogAsync(
                result.UserId,
                ActionCodes.Login,
                ClaimsHelper.GetScreen(HttpContext),
                ClaimsHelper.GetIpAddress(HttpContext),
                $"User {result.Username} logged in"
            );

            return Ok(result);
        }

        // POST /api/auth/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = ClaimsHelper.GetUserId(HttpContext);
            if (userId.HasValue)
                await logService.LogAsync(
                    userId.Value,
                    ActionCodes.Logout,
                    ClaimsHelper.GetScreen(HttpContext),
                    ClaimsHelper.GetIpAddress(HttpContext)
                );

            return NoContent();
        }
    }
}
