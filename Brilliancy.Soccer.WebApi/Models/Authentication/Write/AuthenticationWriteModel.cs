using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Authentication.Write
{
    public class AuthenticationWriteModel
    {
        public int PlayerId { get; set; }

        public string Email { get; set; }
    }
}
