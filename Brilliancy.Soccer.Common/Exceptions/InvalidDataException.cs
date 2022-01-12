using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Exceptions
{
    public class InvalidDataException : CustomException
    {
        public InvalidDataException(string message) : base(message)
        {
        }
    }
}
