using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
