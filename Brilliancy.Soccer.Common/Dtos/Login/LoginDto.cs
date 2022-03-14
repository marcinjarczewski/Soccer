using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Dtos.Login
{
    public class LoginDto
    {
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
