using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string CreditCardMasked { get; set; } = string.Empty;
        
        public string ClientName { get; set; } = string.Empty;
        public string ClientLastname { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

