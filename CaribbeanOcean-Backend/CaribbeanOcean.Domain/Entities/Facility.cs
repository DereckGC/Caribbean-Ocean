using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class Facility
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string features { get; set; } = string.Empty;
        public string image_url { get; set; } = string.Empty;
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }

        public int displayOrder { get; set; }
        public bool active { get; set; } = true;

    }
}
