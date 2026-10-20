using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Reservation
{
    public class UpdateAvailabilityRequestDTO
    {
        public int ReservationId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
