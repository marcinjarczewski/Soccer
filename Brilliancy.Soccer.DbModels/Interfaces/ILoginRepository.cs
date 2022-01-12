using Brilliancy.Soccer.Common.Dtos.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.DbModels.Interfaces
{
    public interface ILoginRepository
    {
        LoginDto GetUser(string login);
    }
}
