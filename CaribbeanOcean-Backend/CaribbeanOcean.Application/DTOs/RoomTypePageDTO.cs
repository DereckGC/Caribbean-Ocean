using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.DTOs.RoomType;

namespace CaribbeanOcean.Application.DTOs
{
    public class RoomTypePageDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<RoomTypeDTO> rooms { get; set; } = new List<RoomTypeDTO>();
    }
}
