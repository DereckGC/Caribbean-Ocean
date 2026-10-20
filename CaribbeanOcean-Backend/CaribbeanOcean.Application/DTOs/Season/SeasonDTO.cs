using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Season
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
