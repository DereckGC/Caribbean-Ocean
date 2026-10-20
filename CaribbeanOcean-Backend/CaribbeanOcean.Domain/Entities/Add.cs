using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class Add
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string image_url { get; set; } = string.Empty;
        public string target_link { get; set; } = string.Empty;
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public bool active { get; set; } = true;
    }
}
