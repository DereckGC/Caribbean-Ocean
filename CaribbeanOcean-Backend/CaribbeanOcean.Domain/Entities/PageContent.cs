using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class PageContent
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string key { get; set; } = string.Empty; // e.g., "about_us", "home_page", etc.

        [Required]
        [StringLength(200)]
        public string title { get; set; } = string.Empty;  

        [StringLength(100)]
        public string subtitle { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public List<MultimediaContent> url_content { get; set; } = new List<MultimediaContent>();

        public int? created_by { get; set; }  

        public int? updated_by { get; set; }

        public bool active { get; set; } = true; // Default value is true

    }
}
