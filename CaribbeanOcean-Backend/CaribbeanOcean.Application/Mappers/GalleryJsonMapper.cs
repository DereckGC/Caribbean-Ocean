using System.Text.Json;
using CaribbeanOcean.Application.DTOs.Multimedia;

namespace CaribbeanOcean.Application.Mappers
{
    internal static class GalleryJsonMapper
    {
        private static readonly JsonSerializerOptions JsonOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

        public static List<MultimediaDTO> FromJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<MultimediaDTO>();

            return JsonSerializer.Deserialize<List<MultimediaDTO>>(json, JsonOptions)
                   ?? new List<MultimediaDTO>();
        }

        public static string ToJson(List<MultimediaDTO> images)
        {
            return JsonSerializer.Serialize(images, JsonOptions);
        }
    }
}