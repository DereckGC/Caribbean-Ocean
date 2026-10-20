using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using Microsoft.Data.SqlClient;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class AddRepository(DbConnectionFactory dbConnectionFactory) : IAddRepository
    {
        public async Task<int> Create(Add add)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateAdd", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@image_url", add.image_url);
                command.Parameters.AddWithValue("@target_link", add.target_link);
                command.Parameters.AddWithValue("@updated_by", add.updated_by);

                await connection!.OpenAsync();

                var newId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(newId);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting offers from database.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                var command = new SqlCommand("sp_DeleteAdd", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                await connection!.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting adds from database");
            }
        }
        public async Task<IEnumerable<Add>> GetAll()
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                var command = new SqlCommand("sp_GetAllAdds", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection!.OpenAsync();

                var adds = new List<Add>();

                Console.WriteLine("Repostiori despues de llamado");

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    adds.Add(MapToAdd(reader));
                }

                return adds;

            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving adds from database");
            }
        }

        public async Task<bool> Update(Add add)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateAdd", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", add.id);
                command.Parameters.AddWithValue("@image_url", add.image_url);
                command.Parameters.AddWithValue("@target_link", add.target_link);
                command.Parameters.AddWithValue("@updated_by", add.updated_by);

                await connection!.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error updating adds from database.");
            }
        }

        public async Task<Add> GetById(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                var command = new SqlCommand("sp_GetAddById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                await connection!.OpenAsync();

                Add add = null;

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    add = MapToAdd(reader);
                }

                return add;

            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving adds from database");
            }
        }

        private Add MapToAdd(SqlDataReader reader)
        {
            return new Add
            {
                id = reader.GetInt32("id"),
                image_url = reader.GetString("image_url"),
                target_link = reader.GetString("target_link"),
                updated_by = reader.GetInt32("updated_by"),
                updated_at = reader.GetDateTime("updated_at"),
                active = reader.GetBoolean("active")
            };
        }
    }
}
