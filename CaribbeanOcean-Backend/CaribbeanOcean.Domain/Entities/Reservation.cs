using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;

        public int RoomId { get; set; }
       

        public string ClientName { get; set; } = string.Empty;
        public string ClientLastname { get; set; } = string.Empty;
        public string MaskedCard { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public decimal TotalAmount { get; set; }

        public bool PricingUsedOffer { get; set; }

        public int? SelectedOfferId { get; set; }

        public bool Active { get; set; }


    }
}
