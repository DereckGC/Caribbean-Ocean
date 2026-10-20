using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class AdminUserRepository(DbConnectionFactory dbConnectionFactory) : IAdminUserRepository
    {
        public async Task<AdminUser?> GetByUsername(string username)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAdminUserByUsername", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);

                await connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (!await reader.ReadAsync()) return null;

                return new AdminUser
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    username = reader.GetString(reader.GetOrdinal("username")),
                    password_hash = reader.GetString(reader.GetOrdinal("password_hash")),
                    name = reader.GetString(reader.GetOrdinal("name")),
                    email = reader.GetString(reader.GetOrdinal("email")),
                    last_login = reader.IsDBNull(reader.GetOrdinal("last_login"))
                                        ? DateTime.MinValue
                                        : reader.GetDateTime(reader.GetOrdinal("last_login"))
                };
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving admin user from database.");
            }
        }

        public async Task<int> AddAdminUser(AdminUser user)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateAdminUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@password_hash", user.password_hash);
                command.Parameters.AddWithValue("@name", user.name);
                command.Parameters.AddWithValue("@email", user.email);

                await connection!.OpenAsync();

                var newId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(newId);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error creating admin user in database.");
            }
        }

        public async Task UpdateLastLogin(AdminUser user)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateAdminUserLastLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@last_login", DateTime.Now);

                await connection!.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error updating admin user in database.");
            }
        }
    }
}