using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Admin
{
    public record LoginResponse(string Token, int UserId, string Username, DateTime ExpiresAt);
}