using CaribbeanOcean.Application.Settings;
using CaribbeanOcean.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaribbeanOcean.Application.Services
{
    public class TokenService(IOptions<JwtSettings> _options)
    {
        public string Generate(AdminUser user)
        {
            var s = _options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name,              user.name),
                new Claim(ClaimTypes.Role,              "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: s.Issuer,
                audience: s.Audience,
                claims: claims,
                expires: GetExpiration(),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpiration() =>
            DateTime.Now.AddMinutes(_options.Value.ExpirationMinutes);
    }
}

