using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Application.DTOs.Pricing
{
    

    public class ReservationNightPriceDetailResultDTO
    {
        //1. Devolver el preview del precio.
        //2. Convertirlo en ReservationNightDetail para guardarlo en BD.
        public DateTime StayDate { get; set; }

        public decimal BasePrice { get; set; }

        public int? SeasonId { get; set; }

        public string? SeasonName { get; set; }

        public decimal SeasonAdjustmentPercentage { get; set; }

        public int? OfferId { get; set; }

        public string? OfferName { get; set; }

        public decimal OfferDiscountPercentage { get; set; }

        public decimal FinalNightPrice { get; set; }
    }
}
