using System;
using System.Collections.Generic;
using System.Text;
// CaribbeanOcean.Infrastructure/Persistence/ActivityLogRepository.cs
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class ActivityLogRepository(DbConnectionFactory context) : IActivityLogRepository
    {
        public async Task LogAsync(int adminUserId, int actionCode, string screen, string ipAddress, string? detail = null)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;

                // Verificar si ya existe un registro idéntico reciente
                using var checkCommand = new SqlCommand(@"
            SELECT COUNT(*) FROM ACTIVITY_LOG al
            INNER JOIN ACTION_CODE ac ON ac.id = al.action_code_id
            WHERE al.admin_user_id = @admin_user_id 
              AND ac.code = @action_code 
              AND al.screen = @screen
              AND al.created_at >= DATEADD(SECOND, -2, SYSDATETIME())", connection);

                checkCommand.Parameters.AddWithValue("@admin_user_id", adminUserId);
                checkCommand.Parameters.AddWithValue("@action_code", actionCode);
                checkCommand.Parameters.AddWithValue("@screen", screen);

                await connection!.OpenAsync();
                var count = (int)await checkCommand.ExecuteScalarAsync();

                if (count > 0) return; // ya existe un registro reciente idéntico, no duplicar

                using var command = new SqlCommand("sp_CreateActivityLog", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@admin_user_id", adminUserId);
                command.Parameters.AddWithValue("@action_code", actionCode);
                command.Parameters.AddWithValue("@screen", screen);
                command.Parameters.AddWithValue("@ip_address", ipAddress);
                command.Parameters.AddWithValue("@detail", detail ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
            catch
            {
                // La bitácora nunca debe interrumpir el flujo principal
            }
        }

        public async Task<IEnumerable<ActivityLogEntry>> GetAllAsync(int? id = null, string? username = null, int? actionCode = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var result = new List<ActivityLogEntry>();

            using var connection = context.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_FilteredActivityLogs", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@username", username ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@actionCode", actionCode ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dateFrom", dateFrom ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@dateTo", dateTo ?? (object)DBNull.Value);

            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ActivityLogEntry
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Username = reader.GetString(reader.GetOrdinal("username")),
                    ActionCode = reader.GetInt32(reader.GetOrdinal("action_code")),
                    ActionName = reader.GetString(reader.GetOrdinal("action_name")),
                    Category = reader.GetString(reader.GetOrdinal("category")),
                    Screen = reader.GetString(reader.GetOrdinal("screen")),
                    IpAddress = reader.GetString(reader.GetOrdinal("ip_address")),
                    Detail = reader.IsDBNull(reader.GetOrdinal("detail")) ? null : reader.GetString(reader.GetOrdinal("detail")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                });
            }

            return result;
        }
    }
}