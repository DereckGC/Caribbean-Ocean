using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Offer
{
    public class OfferAdminDTO
    {
        public int Id { get; set; } = -1;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Decimal Discount { get; set; } = 0;
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int RoomTypeId { get; set; }
        public string? RoomType { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
