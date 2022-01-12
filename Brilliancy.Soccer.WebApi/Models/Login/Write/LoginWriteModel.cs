using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Login.Write
{
    public class LoginWriteModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}
