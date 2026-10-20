using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.Services
{
    public class ActivityLogService(IActivityLogRepository repository)
    {
        public async Task LogAsync(int adminUserId, int actionCode, string screen, string ipAddress, string? detail = null)
            => await repository.LogAsync(adminUserId, actionCode, screen, ipAddress, detail);

        public async Task<IEnumerable<ActivityLogEntry>> GetAllAsync(int? id = null, string? username = null, int? actionCode = null, DateTime? dateFrom = null, DateTime? dateTo = null)
            => await repository.GetAllAsync(id, username, actionCode, dateFrom, dateTo);
    }
}
