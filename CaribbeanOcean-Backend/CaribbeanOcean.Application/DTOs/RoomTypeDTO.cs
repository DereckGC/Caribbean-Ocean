using System;
namespace CaribbeanOcean.Application.DTOs.RoomType
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DailyRate { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<string>? Features { get; set; } = new List<string>();
        public DateTime UpdatedAt { get; set; }
    }
}
