using System;
using System.Collections.Generic;
using System.Data;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using Microsoft.Data.SqlClient;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class FacilityRepository(DbConnectionFactory context) : IFacilityRepository
    {
        public async Task<IEnumerable<Facility>> GetAllFacilities()
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAllFacilities", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                List<Facility> facilities = new();

                while (await reader.ReadAsync())
                {
                    facilities.Add(MapToFacility(reader));
                }

                return facilities;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving facilities from database.");
            }
        }

        public async Task<IEnumerable<Facility>> GetPublicFacilities()
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetPublicFacilities", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                List<Facility> facilities = new();

                while (await reader.ReadAsync())
                {
                    facilities.Add(MapToFacility(reader));
                }

                return facilities;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving public facilities from database.");
            }
        }

        public async Task<Facility?> GetFacilityById(int id)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetFacilityById", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return MapToFacility(reader);
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving facility from database.");
            }
        }

        public async Task<int> CreateFacility(Facility facility)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateFacility", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", facility.name);
                command.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(facility.description) ? DBNull.Value : facility.description);
                command.Parameters.AddWithValue("@features", string.IsNullOrWhiteSpace(facility.features) ? "[]" : facility.features);
                command.Parameters.AddWithValue("@image_url", string.IsNullOrWhiteSpace(facility.image_url) ? DBNull.Value : facility.image_url);
                command.Parameters.AddWithValue("@display_order", facility.displayOrder);
                command.Parameters.AddWithValue("@updated_by", facility.updated_by.HasValue ? facility.updated_by.Value : DBNull.Value);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error creating facility in database.");
            }
        }

        public async Task<bool> UpdateFacility(Facility facility)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateFacility", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", facility.id);
                command.Parameters.AddWithValue("@name", facility.name);
                command.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(facility.description) ? DBNull.Value : facility.description);
                command.Parameters.AddWithValue("@features", string.IsNullOrWhiteSpace(facility.features) ? "[]" : facility.features);
                command.Parameters.AddWithValue("@image_url", string.IsNullOrWhiteSpace(facility.image_url) ? DBNull.Value : facility.image_url);
                command.Parameters.AddWithValue("@display_order", facility.displayOrder);
                command.Parameters.AddWithValue("@updated_by", facility.updated_by.HasValue ? facility.updated_by.Value : DBNull.Value);
                command.Parameters.AddWithValue("@active", facility.active);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error updating facility in database.");
            }
        }

        public async Task<bool> DeleteFacility(int id, int? updatedBy = null)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeleteFacility", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@updated_by", updatedBy.HasValue ? updatedBy.Value : DBNull.Value);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting facility from database.");
            }
        }

        public async Task<bool> ActivateFacility(int id, int? updatedBy = null)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_ActivateFacility", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@updated_by", updatedBy.HasValue ? updatedBy.Value : DBNull.Value);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error activating facility in database.");
            }
        }

        private Facility MapToFacility(SqlDataReader reader)
        {
            return new Facility
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),

                name = reader.GetString(reader.GetOrdinal("name")),

                description = reader.IsDBNull(reader.GetOrdinal("description"))
                    ? string.Empty
                    : reader.GetString(reader.GetOrdinal("description")),

                features = HasColumn(reader, "features") && !reader.IsDBNull(reader.GetOrdinal("features"))
                    ? reader.GetString(reader.GetOrdinal("features"))
                    : "[]",

                image_url = reader.IsDBNull(reader.GetOrdinal("image_url"))
                    ? string.Empty
                    : reader.GetString(reader.GetOrdinal("image_url")),

                displayOrder = HasColumn(reader, "display_order") && !reader.IsDBNull(reader.GetOrdinal("display_order"))
                    ? reader.GetInt32(reader.GetOrdinal("display_order"))
                    : 0,

                updated_at = HasColumn(reader, "updated_at") && !reader.IsDBNull(reader.GetOrdinal("updated_at"))
                    ? reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    : null,

                updated_by = HasColumn(reader, "updated_by") && !reader.IsDBNull(reader.GetOrdinal("updated_by"))
                    ? reader.GetInt32(reader.GetOrdinal("updated_by"))
                    : null,

                active = HasColumn(reader, "active") && !reader.IsDBNull(reader.GetOrdinal("active"))
                    ? reader.GetBoolean(reader.GetOrdinal("active"))
                    : true
            };
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}