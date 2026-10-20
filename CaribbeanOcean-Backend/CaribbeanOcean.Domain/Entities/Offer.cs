using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class Offer
    {
        public int id {  get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;
        public decimal discount_percentage { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int room_type_id { get; set; }
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public string room_type { get; set; } = string.Empty;
        public string url_room_type_image { get; set; } = string.Empty;
        public bool active { get; set; }
    }
}
