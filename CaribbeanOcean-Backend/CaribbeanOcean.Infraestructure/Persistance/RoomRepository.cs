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
    public class RoomRepository(DbConnectionFactory connectionFactory) : IRoomRepository
    {
        public async Task<Room?> getRoomById(int id)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetRoomById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapToRoom(reader);
            }

            return null;
        }

        public async Task<IEnumerable<Room>> getAllRooms()
        {
            var rooms = new List<Room>();

            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetAllRooms", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                rooms.Add(MapToRoom(reader));
            }

            return rooms;
        }

        public async Task<int> createRoom(Room room)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_CreateRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@number", room.Number);
            command.Parameters.AddWithValue("@room_type_id", room.RoomTypeId);
            command.Parameters.AddWithValue("@active", room.Active);

            await connection!.OpenAsync();
            var newId = await command.ExecuteScalarAsync();
            return Convert.ToInt32(newId);
        }

        public async Task<bool> updateRoom(Room room)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_UpdateRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", room.Id);
            command.Parameters.AddWithValue("@number", room.Number);
            command.Parameters.AddWithValue("@room_type_id", room.RoomTypeId);
            command.Parameters.AddWithValue("@active", room.Active);

            await connection!.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> deleteRoom(int id)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_DeleteRoom", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            await connection!.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> HasAvailableRoomByType(int? roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_HasAvailableRoomByType", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@room_type_id", roomTypeId);
            command.Parameters.AddWithValue("@check_in", checkIn);
            command.Parameters.AddWithValue("@check_out", checkOut);

            await connection!.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return result != null && Convert.ToBoolean(result);
        }

        public async Task<int> GetAvailableRoomIdByType(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetAvailableRoomIdByType", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@room_type_id", roomTypeId);
            command.Parameters.AddWithValue("@check_in", checkIn);
            command.Parameters.AddWithValue("@check_out", checkOut);

            await connection!.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                return 0;

            return Convert.ToInt32(result);
        }

        public async Task<int> GetAvailableRoomIdByTypeForUpdate(
                int reservationId,
                int roomTypeId,
                DateTime checkIn,
                DateTime checkOut)
        {
            try
            {
                using var connection = connectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAvailableRoomIdByTypeForUpdate", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@reservation_id", reservationId);
                command.Parameters.AddWithValue("@room_type_id", roomTypeId);
                command.Parameters.AddWithValue("@check_in", checkIn);
                command.Parameters.AddWithValue("@check_out", checkOut);

                await connection!.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(
                    ex,
                    "Error retrieving available room for reservation update."
                );
            }
        }

        public async Task<bool> HasAvailableRoomByTypeForUpdate(int? roomTypeId, DateTime checkIn, DateTime checkOut, int reservationId)
        {
            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_HasAvailableRoomByTypeForUpdate", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@room_type_id", roomTypeId);
            command.Parameters.AddWithValue("@check_in", checkIn);
            command.Parameters.AddWithValue("@check_out", checkOut);
            command.Parameters.AddWithValue("@reservation_id", reservationId);

            await connection!.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return result != null && Convert.ToBoolean(result);
        }


        private Room MapToRoom(SqlDataReader reader)
        {
            return new Room
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Number = reader.GetInt32(reader.GetOrdinal("number")),
                RoomTypeId = reader.GetInt32(reader.GetOrdinal("room_type_id")),
                Active = reader.GetBoolean(reader.GetOrdinal("active")) // maps to BIT in SQL
            };
        }

        
        public async Task<List<AvailableRoomInfo>> GetRoomStatusToday(int? roomTypeId)
        {
            var result = new List<AvailableRoomInfo>();

            using var connection = connectionFactory.CreateConnection() as SqlConnection;
            using var command = new SqlCommand("sp_GetRoomStatusToday", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(
                "@roomTypeId",
                roomTypeId ?? (object)DBNull.Value
            );

            await connection!.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new AvailableRoomInfo
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Number = reader.GetInt32(reader.GetOrdinal("number")),
                    RoomType = reader.GetString(reader.GetOrdinal("room_type")),
                    Active = reader.GetBoolean(reader.GetOrdinal("active")),
                    Status = reader.GetString(reader.GetOrdinal("status"))
                });
            }

            return result;
        }
    }
}
