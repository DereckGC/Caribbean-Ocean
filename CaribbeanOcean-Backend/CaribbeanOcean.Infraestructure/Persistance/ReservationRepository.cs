using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class ReservationRepository(DbConnectionFactory context):IReservationRepository
    {
        private readonly DbConnectionFactory _context = context;

        public async Task<Reservation> getReservationById(int id)
        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetReservationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {

                    return MapToReservation(reader);
                }

                return null;
            }

            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex,"Error retrieving reservation from database.");
            }
        }

        public async Task<IEnumerable<Reservation>> getAllReservations()
        {
            try {
                var reservations = new List<Reservation>();

                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAllReservations", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    reservations.Add(MapToReservation(reader));
                }
                return reservations;
            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw SqlExceptionMapper.Map(ex,"Error retrieving reservations from database.");
            }
            
        }

        public async Task<int> createReservation(Reservation reservation)
        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateReservation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@code", reservation.Code);
                command.Parameters.AddWithValue("@room_id", reservation.RoomId); 
                command.Parameters.AddWithValue("@client_name", reservation.ClientName);
                command.Parameters.AddWithValue("@client_lastname", reservation.ClientLastname);
                command.Parameters.AddWithValue("@masked_card", reservation.MaskedCard);
                command.Parameters.AddWithValue("@email", reservation.Email);
                command.Parameters.AddWithValue("@check_in", reservation.CheckIn);
                command.Parameters.AddWithValue("@check_out", reservation.CheckOut);
                command.Parameters.AddWithValue("@total_amount", reservation.TotalAmount);
                command.Parameters.AddWithValue("@pricing_used_offer",reservation.PricingUsedOffer);
                command.Parameters.AddWithValue("@selected_offer_id", reservation.SelectedOfferId.HasValue? reservation.SelectedOfferId.Value: DBNull.Value);
                await connection!.OpenAsync();
                var newId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(newId);
            } catch (SqlException ex) {
                throw SqlExceptionMapper.Map(ex,"Error creating reservation in database.");
            }
        }

        public async Task<bool> updateReservation(Reservation reservation)
        {
            try
            {
                
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateReservation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", reservation.Id);
                command.Parameters.AddWithValue("@code", reservation.Code);
                command.Parameters.AddWithValue("@room_id", reservation.RoomId);
               
                command.Parameters.AddWithValue("@client_name", reservation.ClientName);
                command.Parameters.AddWithValue("@client_lastname", reservation.ClientLastname);
               
                command.Parameters.AddWithValue("@email", reservation.Email);
                command.Parameters.AddWithValue("@check_in", reservation.CheckIn);
                command.Parameters.AddWithValue("@check_out", reservation.CheckOut);
                command.Parameters.AddWithValue("@total_amount", reservation.TotalAmount);
              
                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            } catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex,"Error updating reservation in database.");
            }
        }

        public async Task<bool> deleteReservation(int id)
        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeleteReservation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                await connection!.OpenAsync();

                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting reservation in database.");
            }
        }

        public async Task<ReservationDetails?> GetReservationDetailsByReservationId(int reservationId)

        {
            try
            {
                using var connection = _context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetReservationDetailsByReservationId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@reservation_id", reservationId);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new ReservationDetails
                    {
                        RoomTypeId = reader.GetInt32(reader.GetOrdinal("room_type_id")),
                        RoomTypeName = reader.GetString(reader.GetOrdinal("room_type_name")),
                       
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex,"Error retrieving reservation details from database.");
            }

        }

        private Reservation MapToReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Code = reader.GetString(reader.GetOrdinal("code")),
                RoomId = reader.GetInt32(reader.GetOrdinal("room_id")),
                
                ClientName = reader.GetString(reader.GetOrdinal("client_name")),
                ClientLastname = reader.GetString(reader.GetOrdinal("client_lastname")),
                MaskedCard = reader.GetString(reader.GetOrdinal("masked_card")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                CheckIn = reader.GetDateTime(reader.GetOrdinal("check_in")),
                CheckOut = reader.GetDateTime(reader.GetOrdinal("check_out")),
                TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount"))
            };
        }
    }
}

