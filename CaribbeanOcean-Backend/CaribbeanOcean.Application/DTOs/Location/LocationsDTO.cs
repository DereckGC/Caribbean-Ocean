using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Multimedia;

namespace CaribbeanOcean.Application.DTOs.Location
{
    public class LocationsDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MultimediaDTO> Images { get; set; } = new List<MultimediaDTO>();
    }
}
