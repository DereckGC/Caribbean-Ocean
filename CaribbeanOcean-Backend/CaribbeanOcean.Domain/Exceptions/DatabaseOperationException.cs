using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Exceptions
{
    
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

//DatabaseOperationException: unexpected database problem