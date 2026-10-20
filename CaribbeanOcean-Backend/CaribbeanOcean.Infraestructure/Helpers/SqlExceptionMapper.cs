using Microsoft.Data.SqlClient;
using CaribbeanOcean.Domain.Exceptions;

namespace CaribbeanOcean.Infrastructure.Persistence.Helpers
{
    public static class SqlExceptionMapper
    {
        public static Exception Map(SqlException ex, string operation)
        {
            return ex.Number switch
            {
                2627 => new ConflictException("A record with the same unique value already exists."),
                2601 => new ConflictException("A duplicate value violates a unique index."),
                547 => new ValidationException("The operation violates a database constraint or related record rule."),
                515 => new ValidationException("A required field is missing."),
                245 => new ValidationException("One of the provided values has an invalid format."),
                8115 => new ValidationException("One of the numeric values is out of range."),
                1205 => new DatabaseOperationException("The database operation was interrupted by a deadlock. Please try again.", ex),
                _ => new DatabaseOperationException($"Database error while {operation}.", ex)
            };
        }
    }
}