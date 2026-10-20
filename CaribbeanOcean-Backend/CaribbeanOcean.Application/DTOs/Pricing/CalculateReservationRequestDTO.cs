using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Pricing
{
    //Calcula el precio de esta habitación, en estas fechas, usando o no usando oferta.
    public class CalculateReservationPriceRequestDTO
    {
        public int RoomTypeId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public bool ApplyOffers { get; set; }

        public int? SelectedOfferId { get; set; }
    }
}
