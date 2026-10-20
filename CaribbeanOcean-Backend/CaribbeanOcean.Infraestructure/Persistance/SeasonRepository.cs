using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using Microsoft.Data.SqlClient;

namespace CaribbeanOcean.Infraestructure.Persistance
{
  public class SeasonRepository (DbConnectionFactory connectionFactory): ISeasonRepository
    {
       public async Task<Season?> getSeasonById(int id)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetSeasonById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapToSeason(reader);
            }
            return null;
        }
        public async Task<IEnumerable<Season>> getAllSeasons()
        {
            var seasons = new List<Season>();
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetAllSeasons", connection);
            command.CommandType = CommandType.StoredProcedure;
            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                seasons.Add(MapToSeason(reader));
            }
            return seasons;
        }

        public async Task<int> createSeason(Season season)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_CreateSeason", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", season.name);
            command.Parameters.AddWithValue("@StartDate", season.startDate);
            command.Parameters.AddWithValue("@EndDate", season.endDate);
            command.Parameters.AddWithValue("@Discount", season.discountAmount);
            await connection!.OpenAsync();
            return (int)await command.ExecuteScalarAsync();
        }

        public async Task <bool> updateSeason(Season season)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_UpdateSeason", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", season.id);
            command.Parameters.AddWithValue("@name", season.name);
            command.Parameters.AddWithValue("@StartDate", season.startDate);
            command.Parameters.AddWithValue("@EndDate", season.endDate);
            command.Parameters.AddWithValue("@Discount", season.discountAmount);

            await connection!.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;


        }

        public async Task <bool> deleteSeason(int id)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_DeleteSeason", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            await connection!.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<Season?> GetActiveSeasonByDate(DateTime date)
        {
            try
            {
                using var connection = connectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetActiveSeasonByDate", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@date", date.Date);

                await connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return MapToSeason(reader);
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving active season from database.");
            }
        }



        private Season MapToSeason(SqlDataReader reader)
        {
            return new Season
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                name = reader.GetString(reader.GetOrdinal("name")),
                startDate = reader.GetDateTime(reader.GetOrdinal("start_date")),
                endDate = reader.GetDateTime(reader.GetOrdinal("end_date")),
                discountAmount = reader.GetDecimal(reader.GetOrdinal("discount_amount"))
            };
        }
    }
}
