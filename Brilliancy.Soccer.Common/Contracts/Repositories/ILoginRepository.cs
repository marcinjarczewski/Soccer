using Brilliancy.Soccer.Common.Dtos.Login;

namespace Brilliancy.Soccer.Common.Contracts.Repositories
{
    public interface ILoginRepository
    {
        LoginDto GetUserForUserManager(string login);
    }
}
