﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Models.Authentication.Read
{
    public class AuthenticationModel
    {
        public string Message { get; set; }

        public bool IsKeyValid { get; set; }

        public int AuthId { get; set; }
    }
}
