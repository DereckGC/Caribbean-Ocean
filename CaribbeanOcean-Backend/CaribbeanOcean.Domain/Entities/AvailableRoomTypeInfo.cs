using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class AvailableRoomTypeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal DailyRate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
        public int AvailableRooms { get; set; }
    }
}
