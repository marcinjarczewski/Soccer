using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface ILoginModule
    {
        UserDto GetUser(string login);

        UserDto GetUser(int id);

        void RegisterUser(RegisterUserDto dto);
    }
}
