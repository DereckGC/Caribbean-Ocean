using System;
namespace CaribbeanOcean.Domain.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
       
        public string? Description { get; set; } // can be null 
        
        
        public decimal DailyRate { get; set; }
        
        public string? features { get; set; }
        public string? ImageUrl { get; set; } //can be null
        
        public DateTime UpdatedAt { get; set; }

        public bool Active { get; set; }
    }
}