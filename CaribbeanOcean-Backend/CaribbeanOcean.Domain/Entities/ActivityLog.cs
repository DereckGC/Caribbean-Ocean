using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public int AdminUserId { get; set; }
        public int ActionCodeId { get; set; }
        public string Screen { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
