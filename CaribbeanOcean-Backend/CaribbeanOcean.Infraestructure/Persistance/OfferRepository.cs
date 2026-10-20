using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class OfferRepository(DbConnectionFactory dbConnectionFactory) : IOfferRepository
    {
        public async Task<int> Create(Offer offer)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateOffer", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", offer.name);
                command.Parameters.AddWithValue("@description", offer.description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@discount_percentage", offer.discount_percentage);
                command.Parameters.AddWithValue("@start_time", offer.start_time);
                command.Parameters.AddWithValue("@end_time", offer.end_time);
                command.Parameters.AddWithValue("@room_type_id", offer.room_type_id);
                command.Parameters.AddWithValue("@update_by", offer.updated_by ?? (object)DBNull.Value);

                await connection!.OpenAsync();

                var newId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(newId);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error creating offer in database.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeleteOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                await connection!.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting offers from database");
            }
        }

        public async Task<IEnumerable<Offer>> GetAll()
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAllOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection!.OpenAsync();

                var offers = new List<Offer>();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    offers.Add(MapToOffer(reader));
                }

                return offers;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving offers from database");
            }
        }

        public async Task<IEnumerable<Offer>> GetClientOffers()
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetClientOffers", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection!.OpenAsync();

                var offers = new List<Offer>();

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    offers.Add(MapToOffer(reader));
                }

                return offers;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving offers from database");
            }
        }

        public async Task<Offer?> GetById(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetOfferById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                await connection!.OpenAsync();

                Offer? offer = null;

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    offer = MapToOffer(reader);
                }

                return offer;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving offers from database");
            }
        }

        public async Task<bool> Update(Offer offer)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateOffer", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", offer.id);
                command.Parameters.AddWithValue("@name", offer.name);
                command.Parameters.AddWithValue("@description", offer.description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@discount_percentage", offer.discount_percentage);
                command.Parameters.AddWithValue("@start_time", offer.start_time);
                command.Parameters.AddWithValue("@end_time", offer.end_time);
                command.Parameters.AddWithValue("@room_type_id", offer.room_type_id);
                command.Parameters.AddWithValue("@updated_by", offer.updated_by ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@updated_at", DateTime.Now);

                await connection!.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error updating offers from database.");
            }
        }

        public async Task<Offer?> GetApplicableOfferById(int offerId, int roomTypeId, DateTime date)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetApplicableOfferById", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@offer_id", offerId);
                command.Parameters.AddWithValue("@room_type_id", roomTypeId);
                command.Parameters.AddWithValue("@date", date.Date);

                await connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return MapToOffer(reader);
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving applicable offer from database.");
            }
        }

        public async Task<bool> ValidateOfferRegistration(Offer offer)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_ValidateOffer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@room_type_id", offer.room_type_id);
                command.Parameters.AddWithValue("@start_time", offer.start_time);
                command.Parameters.AddWithValue("@end_time", offer.end_time);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                if (Convert.ToInt32(result) > 0)
                {
                    throw new ValidationException("There is already an offer allocated for those dates in that type of room.");
                }

                return true;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error Validating Offer.");
            }
        }

        private Offer MapToOffer(SqlDataReader reader)
        {
            return new Offer
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                name = reader.GetString(reader.GetOrdinal("name")),
                description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString(reader.GetOrdinal("description")),
                discount_percentage = reader.GetDecimal(reader.GetOrdinal("discount_percentage")),
                start_time = reader.GetDateTime(reader.GetOrdinal("start_time")),
                end_time = reader.GetDateTime(reader.GetOrdinal("end_time")),
                room_type_id = reader.GetInt32(reader.GetOrdinal("room_type_id")),
                updated_at = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("updated_at")),
                updated_by = reader.IsDBNull(reader.GetOrdinal("updated_by")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("updated_by")),
                room_type = reader.IsDBNull(reader.GetOrdinal("room_type")) ? string.Empty : reader.GetString(reader.GetOrdinal("room_type")),
                url_room_type_image = reader.IsDBNull(reader.GetOrdinal("url_room_type_image")) ? string.Empty : reader.GetString(reader.GetOrdinal("url_room_type_image")),
                active = reader.GetBoolean(reader.GetOrdinal("active"))
            };
        }
    }
}