using System;
using System.Collections.Generic;
using System.Text;

using CaribbeanOcean.Domain.Entities;

namespace CaribbeanOcean.Domain.Interfaces
{
    public interface IActivityLogRepository
    {
        Task LogAsync(int adminUserId, int actionCode, string screen, string ipAddress, string? detail = null);
        Task<IEnumerable<ActivityLogEntry>> GetAllAsync(int? id = null, string? username = null, int? actionCode = null, DateTime? dateFrom = null, DateTime? dateTo = null);
    }

    public class ActivityLogEntry
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int ActionCode { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Screen { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
