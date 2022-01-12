using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Exceptions
{
    public class UserDataException : CustomException
    {
        public UserDataException(string message) : base(message)
        {
        }
    }
}
