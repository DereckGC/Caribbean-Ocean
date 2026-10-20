using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CaribbeanOcean.Domain.Exceptions;


namespace CaribbeanOcean.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found.");

                await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error.");

                await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Conflict error.");

                await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message);
            }
            
            catch (DatabaseOperationException ex)
            {
                _logger.LogError(ex, "Database operation error.");

                await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "A database error occurred while processing the request.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception.");

                await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private static async Task WriteErrorResponseAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}