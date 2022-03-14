using Brilliancy.Soccer.Common.Dtos.Login;
using System.Security.Claims;

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
