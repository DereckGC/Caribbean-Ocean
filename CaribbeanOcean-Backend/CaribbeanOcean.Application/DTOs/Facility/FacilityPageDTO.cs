using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Multimedia;

namespace CaribbeanOcean.Application.DTOs.Facility
{
    public class FacilityPageDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<FacilityDTO> Facilities { get; set; } = new List<FacilityDTO>();
    }
}
