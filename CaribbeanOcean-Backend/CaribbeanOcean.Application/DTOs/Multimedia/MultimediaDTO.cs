using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CaribbeanOcean.Application.DTOs.Multimedia
{
    public class MultimediaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; } = string.Empty;

        [JsonPropertyName("alt")]
        public string Alt { get; set; } = string.Empty;

        [JsonPropertyName("publicId")]
        public string PublicId { get; set; } = string.Empty;
    }
}
