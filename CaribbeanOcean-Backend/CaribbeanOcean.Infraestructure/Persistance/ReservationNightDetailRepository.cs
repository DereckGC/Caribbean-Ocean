using System.Data;
using Microsoft.Data.SqlClient;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class ReservationNightDetailRepository(DbConnectionFactory context)
        : IReservationNightDetailRepository
    {
        private readonly DbConnectionFactory _context = context;

        public async Task AddRange(
            int reservationId,
            List<ReservationNightDetail> nights)
        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;

                await connection!.OpenAsync();

                foreach (var night in nights)
                {
                    using var command = new SqlCommand("sp_CreateReservationNightDetail", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@reservation_id", reservationId);
                    command.Parameters.AddWithValue("@stay_date", night.StayDate.Date);
                    command.Parameters.AddWithValue("@base_price", night.BasePrice);

                    command.Parameters.AddWithValue(
                        "@season_id",
                        night.SeasonId.HasValue
                            ? night.SeasonId.Value
                            : DBNull.Value
                    );

                    command.Parameters.AddWithValue(
                        "@season_name",
                        string.IsNullOrWhiteSpace(night.SeasonName)
                            ? DBNull.Value
                            : night.SeasonName
                    );

                    command.Parameters.AddWithValue(
                        "@season_adjustment_percentage",
                        night.SeasonAdjustmentPercentage
                    );

                    command.Parameters.AddWithValue(
                        "@offer_id",
                        night.OfferId.HasValue
                            ? night.OfferId.Value
                            : DBNull.Value
                    );

                    command.Parameters.AddWithValue(
                        "@offer_name",
                        string.IsNullOrWhiteSpace(night.OfferName)
                            ? DBNull.Value
                            : night.OfferName
                    );

                    command.Parameters.AddWithValue(
                        "@offer_discount_percentage",
                        night.OfferDiscountPercentage
                    );

                    command.Parameters.AddWithValue(
                        "@final_night_price",
                        night.FinalNightPrice
                    );

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(
                    ex,
                    "Error creating reservation night details in database."
                );
            }
        }

        public async Task DeactivateByReservationId(int reservationId)
        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeactivateReservationNightDetails", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@reservation_id", reservationId);

                await connection!.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(
                    ex,
                    "Error deactivating reservation night details in database."
                );
            }
        }

        public async Task<List<ReservationNightDetail>> GetActiveByReservationId(int reservationId)
        {
            try
            {
                var details = new List<ReservationNightDetail>();

                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetActiveReservationNightDetails", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@reservation_id", reservationId);

                await connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    details.Add(MapToReservationNightDetail(reader));
                }

                return details;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(
                    ex,
                    "Error retrieving reservation night details from database."
                );
            }
        }

        private ReservationNightDetail MapToReservationNightDetail(SqlDataReader reader)
        {
            return new ReservationNightDetail
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                ReservationId = reader.GetInt32(reader.GetOrdinal("reservation_id")),
                StayDate = reader.GetDateTime(reader.GetOrdinal("stay_date")),
                BasePrice = reader.GetDecimal(reader.GetOrdinal("base_price")),

                SeasonId = reader.IsDBNull(reader.GetOrdinal("season_id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("season_id")),

                SeasonName = reader.IsDBNull(reader.GetOrdinal("season_name"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("season_name")),

                SeasonAdjustmentPercentage = reader.GetDecimal(
                    reader.GetOrdinal("season_adjustment_percentage")
                ),

                OfferId = reader.IsDBNull(reader.GetOrdinal("offer_id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("offer_id")),

                OfferName = reader.IsDBNull(reader.GetOrdinal("offer_name"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("offer_name")),

                OfferDiscountPercentage = reader.GetDecimal(
                    reader.GetOrdinal("offer_discount_percentage")
                ),

                FinalNightPrice = reader.GetDecimal(reader.GetOrdinal("final_night_price")),
                Active = reader.GetBoolean(reader.GetOrdinal("active")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
            };
        }
    }
}