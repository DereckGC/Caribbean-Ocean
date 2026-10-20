using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Room
{
    public class UpdateRoomDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
        public bool Active { get; set; }
    }
}
