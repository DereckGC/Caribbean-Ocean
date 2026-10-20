using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

//NotFoundException: something does not exist