using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Offer
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public string UrlImage {  get; set; } = string.Empty;

        public int RoomTypeId { get; set; }
    }
}
