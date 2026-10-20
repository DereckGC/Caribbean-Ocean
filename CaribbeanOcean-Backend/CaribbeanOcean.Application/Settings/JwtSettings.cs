using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public int ExpirationMinutes { get; set; } = 30;
    }
}
