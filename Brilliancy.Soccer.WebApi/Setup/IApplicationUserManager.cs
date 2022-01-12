using Brilliancy.Soccer.Common.Dtos.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Brilliancy.Soccer.DbModels.Interfaces
{
    public interface IApplicationUserManager
    {
        string GetIdToken(int id, string login);

        string GetAccessToken(string username);

        string GeneratePassword(string candidate);

        LoginDto LoginUser(string login, string password);

        ClaimsPrincipal AuthorizeUser(string name, string email);
    }
}
