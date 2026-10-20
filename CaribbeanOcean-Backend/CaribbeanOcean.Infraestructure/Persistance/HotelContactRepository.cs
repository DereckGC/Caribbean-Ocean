using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infrastructure.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Json;

namespace CaribbeanOcean.Infraestructure.Persistance
{
    public class HotelContactRepository(DbConnectionFactory dbConnectionFactory) : IHotelContactRepository
    {
        public async Task<int> Create(HotelContact hotelContact)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreateHotelContact", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@type", hotelContact.type);
                command.Parameters.AddWithValue("@contact", hotelContact.contact);

                await connection!.OpenAsync();
                var newId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(newId);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error creating hotel contact in database.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeleteHotelContact", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting hotel contact from database.");
            }
        }

        public async Task<IEnumerable<HotelContact>> GetAll()
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAllHotelContacts", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                var hotelContacts = new List<HotelContact>();
                while (await reader.ReadAsync())
                {
                    hotelContacts.Add(new HotelContact
                    {
                        id = reader.GetInt32("id"),
                        type = reader.GetString("type"),
                        contact = reader.GetString("contact")
                    });
                }
                return hotelContacts;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving hotel contacts from database.");
            }
        }

        public async Task<HotelContact> GetById(int id)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetHotelContactById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new HotelContact
                    {
                        id = reader.GetInt32("Id"),
                        type = reader.GetString("Type"),
                        contact = reader.GetString("Contact")
                    };
                }
                throw new InvalidOperationException("Hotel contact not found");
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, $"Error retrieving hotel contact with ID {id} from database.");
            }
        }

        public async Task<bool> Update(HotelContact hotelContact)
        {
            try
            {
                using var connection = dbConnectionFactory.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateHotelContact", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", hotelContact.id);
                command.Parameters.AddWithValue("@type", hotelContact.type);
                command.Parameters.AddWithValue("@contact", hotelContact.contact);

                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error updating hotel contact in database.");
            }
        }
    }
}