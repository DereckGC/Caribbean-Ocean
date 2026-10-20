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
    public class PageContentRepository(DbConnectionFactory context) : IPageContentRepository
    {
        public async Task<IEnumerable<PageContent>> GetAllPageContents()
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetAllPageContents", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                List<PageContent> pageContents = new List<PageContent>();
                while (await reader.ReadAsync())
                {
                    pageContents.Add(MapToPageContent(reader));
                }
                return pageContents;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving page contents from database.");
            }
        }

        public async Task<PageContent> GetPageContentById(int id)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetPageContentById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                PageContent pageContent = null;
                while (await reader.ReadAsync())
                {
                    pageContent = MapToPageContent(reader);
                }
                return pageContent;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving page contents from database.");
            }
        }

        public async Task<PageContent> GetPageContentByTitle(string title)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_GetPageContentByKey", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@key", title);

                await connection!.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                PageContent pageContent = null;
                while (await reader.ReadAsync())
                {
                    pageContent = MapToPageContent(reader);
                }
                return pageContent;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving page contents from database.");
            }
        }

        public async Task<int> CreatePageContent(PageContent pageContent)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_CreatePageContent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@title", pageContent.title);
                command.Parameters.AddWithValue("@subtitle", pageContent.subtitle);
                command.Parameters.AddWithValue("@description", pageContent.description);
                command.Parameters.AddWithValue("@url_content", JsonSerializer.Serialize(pageContent.url_content));
                command.Parameters.AddWithValue("@created_by", pageContent.created_by);

                await connection!.OpenAsync();
                var newId = await command.ExecuteScalarAsync();

                return Convert.ToInt32(newId);
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving reservation from database.");
            }
        }

        public async Task<bool> UpdatePageContent(PageContent pageContent)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdatePageContent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", pageContent.id);
                command.Parameters.AddWithValue("@title", pageContent.title);
                command.Parameters.AddWithValue("@subtitle", pageContent.subtitle);
                command.Parameters.AddWithValue("@description", pageContent.description);
                command.Parameters.AddWithValue("@url_content", JsonSerializer.Serialize(pageContent.url_content));
                command.Parameters.AddWithValue("@updated_by", (object?)pageContent.updated_by ?? DBNull.Value);

                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error retrieving reservation from database.");
            }
        }

        public async Task<bool> DeletePageContent(int id)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_DeletePageContent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(ex, "Error deleting page content from database.");
            }
        }

        private PageContent MapToPageContent(SqlDataReader reader)
        {
            string url_content_string = reader.IsDBNull(reader.GetOrdinal("url_content"))
                ? "[]"
                : reader.GetString(reader.GetOrdinal("url_content"));

            return new PageContent
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                key = reader.GetString(reader.GetOrdinal("key")),
                title = reader.IsDBNull(reader.GetOrdinal("title")) ? string.Empty : reader.GetString(reader.GetOrdinal("title")),
                subtitle = reader.IsDBNull(reader.GetOrdinal("subTitle")) ? string.Empty : reader.GetString(reader.GetOrdinal("subTitle")),
                description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString(reader.GetOrdinal("description")),
                url_content = JsonSerializer.Deserialize<List<MultimediaContent>>(url_content_string) ?? new List<MultimediaContent>(),
                updated_by = reader.IsDBNull(reader.GetOrdinal("updated_by")) ? null : reader.GetInt32(reader.GetOrdinal("updated_by")),
                active = reader.GetBoolean(reader.GetOrdinal("active"))
            };
        }

        public async Task<bool> UpdateGalleryContent(List<MultimediaContent> images, int adminId)
        {
            try
            {
                using var connection = context.CreateConnection() as SqlConnection;
                using var command = new SqlCommand("sp_UpdateGalleryContent", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(
                    "@url_content",
                    JsonSerializer.Serialize(images)
                );
                command.Parameters.AddWithValue("@updated_by", adminId);

                await connection!.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw SqlExceptionMapper.Map(
                    ex,
                    "Error updating gallery content."
                );
            }
        }


    }
}