using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CaribbeanOcean.Application.UseCases.Authentication
{
    public class Authenticate(IConfiguration _configuration, IAdminUserRepository _repository)
    {
        private readonly PasswordHasher<AdminUser> _hasher = new();

        public async Task<AdminUser?> ExecuteAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _repository.GetByUsername(username);
            if (user is null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.password_hash, password);
            if (result == PasswordVerificationResult.Failed) return null;

            await _repository.UpdateLastLogin(user);

            return user;
        }
    }
}
