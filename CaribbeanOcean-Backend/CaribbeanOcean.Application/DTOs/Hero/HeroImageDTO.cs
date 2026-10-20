using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Hero
{
    public class AddHeroImageDTO
    {
        public string Alt { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } 
    }

    public class UpdateHeroImageDTO
    {
        public string Alt { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } 
    }
}
