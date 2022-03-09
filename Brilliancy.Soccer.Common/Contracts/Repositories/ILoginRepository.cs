using Brilliancy.Soccer.Common.Dtos.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Repositories
{
    public interface ILoginRepository
    {
        LoginDto GetUserForUserManager(string login);
    }
}
