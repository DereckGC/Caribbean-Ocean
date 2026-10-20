using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs
{
    public class RoomTypeRequestDTO
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
