using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Facility
{
    public class UpdateFacilityDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<string> Features { get; set; } = new();

        public string? ImageUrl { get; set; }

        public int updated_by { get; set; }
        public int DisplayOrder { get; set; }

        public bool Active { get; set; }
    }
}
