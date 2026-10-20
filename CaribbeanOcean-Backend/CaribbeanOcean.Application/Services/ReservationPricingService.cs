using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs.Pricing;
using CaribbeanOcean.Application.DTOs.Room;
using CaribbeanOcean.Application.UseCases;

namespace CaribbeanOcean.Application.Services
{
    public class ReservationPricingService(
       CalculatePrice calculatePrice
       )
    {
        public async Task<ReservationPriceResultDTO> Calculate(CalculateReservationPriceRequestDTO request)
            => await calculatePrice.ExecuteAsync(request);
    }

}

   

    
