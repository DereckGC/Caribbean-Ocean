using CaribbeanOcean.Application.DTOs.Admin;
using CaribbeanOcean.Application.UseCases.Authentication;
using Microsoft.Extensions.Logging;

namespace CaribbeanOcean.Application.Services
{
    public class AuthService (Register _register, Authenticate _authenticate, TokenService _tokenService, ILogger<AuthService> _logger)
    {
        public async Task<int> Register(RegisterRequestDTO dto) => await _register.ExecuteAsync(dto);

        public async Task<LoginResponse?> Login(string username, string password)
        {
            var user = await _authenticate.ExecuteAsync(username, password);

            if (user is null)
            {
                _logger.LogWarning("Failed login attempt for user {username}", username);
                return null;
            }

            var token = _tokenService.Generate(user);
            var expires = _tokenService.GetExpiration();

            return new LoginResponse(token, user.id, user.username, expires);
        }
    }
}
