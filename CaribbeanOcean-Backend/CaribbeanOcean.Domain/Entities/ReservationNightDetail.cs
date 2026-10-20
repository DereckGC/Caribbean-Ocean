using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class ReservationNightDetail
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public DateTime StayDate { get; set; }

        public decimal BasePrice { get; set; }

        public int? SeasonId { get; set; }

        public string? SeasonName { get; set; }

        public decimal SeasonAdjustmentPercentage { get; set; }

        public int? OfferId { get; set; }

        public string? OfferName { get; set; }

        public decimal OfferDiscountPercentage { get; set; }

        public decimal FinalNightPrice { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
