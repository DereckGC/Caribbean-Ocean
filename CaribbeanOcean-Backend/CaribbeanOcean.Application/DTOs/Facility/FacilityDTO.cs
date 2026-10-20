using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Facility
{
    public class FacilityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new List<string>();
        public string Image_Url { get; set; } = string.Empty;
        public DateTime? Updated_at { get; set; }
        public int? Updated_By { get; set; }

        public int DisplayOrder { get; set; }
        public bool Active { get; set; } = true;
    }
}
