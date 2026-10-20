using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Multimedia;

namespace CaribbeanOcean.Application.DTOs.Gallery
{
    public class GalleryDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MultimediaDTO> Images { get; set; } = new List<MultimediaDTO>();
    }
}
