using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class HotelContact
    {
        public int id { get; set; }
        public string type { get; set; }= string.Empty;
        public string contact { get; set; } = string.Empty;
    }
}
