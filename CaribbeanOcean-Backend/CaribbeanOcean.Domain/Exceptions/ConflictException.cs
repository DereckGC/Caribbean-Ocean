using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}
//ConflictException: the request is valid, but cannot be completed because of the current state