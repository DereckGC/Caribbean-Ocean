using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs
{
    public class CreateReservationDTO
    {
        //  RoomTypeId because that's all the frontend knows
        public int RoomTypeId { get; set; }

        
        public string ClientName { get; set; } = string.Empty;
        public string ClientLastname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool ApplyOffers { get; set; }

        public int? SelectedOfferId { get; set; }
        
    }
}
