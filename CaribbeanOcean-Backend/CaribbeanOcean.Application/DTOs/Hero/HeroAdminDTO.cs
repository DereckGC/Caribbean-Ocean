using CaribbeanOcean.Application.DTOs.Multimedia;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Hero
{
    public class HeroAdminDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MultimediaDTO> Images { get; set; } = new List<MultimediaDTO>();
        public int UpdateBy { get; set; }
    }
}
