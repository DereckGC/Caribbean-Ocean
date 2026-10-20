namespace CaribbeanOcean.Application.DTOs
{
    public class UpdateRoomTypeDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DailyRate { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<string>? Features { get; set; }
    }
}