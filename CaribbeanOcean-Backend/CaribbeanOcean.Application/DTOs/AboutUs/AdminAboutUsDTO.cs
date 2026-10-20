using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.AboutUs
{
    public class AdminAboutUsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UpdateBy { get; set; }
    }
}
