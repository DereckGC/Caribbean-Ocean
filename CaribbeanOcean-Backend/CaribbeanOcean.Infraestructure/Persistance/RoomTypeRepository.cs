using System.Data;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using Microsoft.Data.SqlClient;

public class RoomTypeRepository(DbConnectionFactory connectionFactory) : IRoomTypeRepository
{
    public async Task<RoomType?> getRoomTypeById(int id)
    {
        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_GetRoomTypeById", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@id", id);

        await connection!.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapToRoomType(reader);
        }

        return null;
    }

    public async Task<IEnumerable<RoomType>> getAllRoomTypes()
    {
        var roomTypes = new List<RoomType>();

        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_GetAllRoomTypes", connection);
        command.CommandType = CommandType.StoredProcedure;

        await connection!.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            roomTypes.Add(MapToRoomType(reader));
        }

        return roomTypes;
    }

    public async Task<int> createRoomType(RoomType roomType)
    {
        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_CreateRoomType", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@name", roomType.Name);
        command.Parameters.AddWithValue("@description", (object?)roomType.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@daily_rate", roomType.DailyRate);
        command.Parameters.AddWithValue("@image_url", (object?)roomType.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("@features", (object?)roomType.features ?? DBNull.Value); 

        await connection!.OpenAsync();
        var newId = await command.ExecuteScalarAsync();
        return Convert.ToInt32(newId);
    }

    public async Task<bool> updateRoomType(RoomType roomType)
    {
        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_UpdateRoomType", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@id", roomType.Id);
        command.Parameters.AddWithValue("@name", roomType.Name);
        command.Parameters.AddWithValue("@description", (object?)roomType.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@daily_rate", roomType.DailyRate);
        command.Parameters.AddWithValue("@image_url", (object?)roomType.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("@features", (object?)roomType.features ?? DBNull.Value); 

        await connection!.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> deleteRoomType(int id)
    {
        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_DeleteRoomType", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@id", id);

        await connection!.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    // aqui para arriba 
    public async Task<RoomType?> GetAvailableRoomTypeById(int? roomTypeId, DateTime checkIn, DateTime checkOut)
    {
        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_GetAvailableRoomTypeById", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@room_type_id", roomTypeId);
        command.Parameters.AddWithValue("@check_in", checkIn);
        command.Parameters.AddWithValue("@check_out", checkOut);

        await connection!.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapToRoomType(reader);
        }

        return null;
    }

    public async Task<IEnumerable<RoomType>> GetAllAvailableRoomTypes(DateTime checkIn, DateTime checkOut)
    {
        var roomTypes = new List<RoomType>();

        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_GetAllAvailableRoomTypes", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@check_in", checkIn);
        command.Parameters.AddWithValue("@check_out", checkOut);

        await connection!.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            roomTypes.Add(MapToRoomType(reader));
        }

        return roomTypes;
    }

    public async Task<IEnumerable<AvailableRoomTypeInfo>> GetAllAvailableRoomTypesWithCount(DateTime checkIn, DateTime checkOut)
    {
        var roomTypes = new List<AvailableRoomTypeInfo>();

        using var connection = connectionFactory.CreateConnection() as SqlConnection;
        using var command = new SqlCommand("sp_GetAllAvailableRoomTypesWithCount", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@check_in", checkIn);
        command.Parameters.AddWithValue("@check_out", checkOut);

        await connection!.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            roomTypes.Add(MapToAvailableRoomTypeInfo(reader));
        }

        return roomTypes;
    }

    private RoomType MapToRoomType(SqlDataReader reader)
    {
        return new RoomType
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
            DailyRate = reader.GetDecimal(reader.GetOrdinal("daily_rate")),
            ImageUrl = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString(reader.GetOrdinal("image_url")),
            features = reader.IsDBNull(reader.GetOrdinal("features")) ? null : reader.GetString(reader.GetOrdinal("features")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }

    private AvailableRoomTypeInfo MapToAvailableRoomTypeInfo(SqlDataReader reader)
    {
        return new AvailableRoomTypeInfo
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Description = reader.IsDBNull(reader.GetOrdinal("description"))
                ? null
                : reader.GetString(reader.GetOrdinal("description")),
            DailyRate = reader.GetDecimal(reader.GetOrdinal("daily_rate")),
            ImageUrl = reader.IsDBNull(reader.GetOrdinal("image_url"))
                ? null
                : reader.GetString(reader.GetOrdinal("image_url")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at")),
            Active = reader.GetBoolean(reader.GetOrdinal("active")),
            AvailableRooms = reader.GetInt32(reader.GetOrdinal("available_rooms"))
        };
    }
}
