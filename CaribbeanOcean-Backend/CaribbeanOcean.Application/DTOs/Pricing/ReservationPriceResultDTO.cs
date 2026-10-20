using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Entities;
namespace CaribbeanOcean.Application.DTOs.Pricing
{
    public class ReservationPriceResultDTO
    {
        public decimal TotalAmount { get; set; }

        public int NightCount { get; set; }

        public decimal AverageNightlyRate { get; set; }

        public List<ReservationNightPriceDetailResultDTO> Nights { get; set; } = new();
    }
}
