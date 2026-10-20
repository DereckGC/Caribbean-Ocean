using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class Season
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public decimal discountAmount { get; set; }
    }
}
