using Brilliancy.Soccer.Common.Dtos.User;


namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface ILoginModule
    {
        UserDto GetUser(string login);

        void RegisterUser(RegisterUserDto dto);
    }
}
