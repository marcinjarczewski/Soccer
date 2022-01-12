using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Exceptions
{
    public abstract class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
