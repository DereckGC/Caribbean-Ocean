using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Multimedia;

namespace CaribbeanOcean.Application.DTOs.Location
{
    public class AdminLocationsDTO
    {
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MultimediaDTO> Images { get; set; } = new List<MultimediaDTO>();
        public int UpdateBy { get; set; }
    }
}
