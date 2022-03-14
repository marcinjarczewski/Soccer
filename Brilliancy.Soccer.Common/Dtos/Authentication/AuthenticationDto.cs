using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Authentication
{
    public class AuthenticationDto
    {
        public int PlayerId { get; set; }

        public string Email { get; set; }
    }
}
