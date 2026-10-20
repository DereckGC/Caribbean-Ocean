using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CaribbeanOcean.Application.DTOs.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
        public bool Active { get; set; }
    }
}
