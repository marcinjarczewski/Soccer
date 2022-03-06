using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class LoginModule : BaseModule, ILoginModule, ILoginRepository
    {
        private SoccerDbContext _dbContext {get;}
        public LoginModule(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }

        public UserDto GetUser(string login)
        {
            if(string.IsNullOrEmpty(login))
            {
                throw new InvalidDataException(CoreTranslations.EmptyLogin);
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if(user == null || !user.IsActive)
            {
                throw new UserDataException(CoreTranslations.NoUser);
            }

            return _mapper.Map<UserDto>(user);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.Login))
            {
                throw new InvalidDataException(CoreTranslations.EmptyLogin);
            }

            var oldUser = this._dbContext.Users.FirstOrDefault(u => u.Login.ToLower() == dto.Login.ToLower());
            if(oldUser != null)
            {
                throw new UserDataException(CoreTranslations.LoginInUse);
            }

            var user = _mapper.Map<UserDbModel>(dto);
            user.IsActive = true;
            this._dbContext.Users.Add(user);
            this._dbContext.SaveChanges();
        }

        public LoginDto GetUserForUserManager(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new InvalidDataException(CoreTranslations.EmptyLogin);
            }

            var user = _dbContext.Users.Include(u => u.UserRoles).FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
            {
                throw new UserDataException(CoreTranslations.LoginInUse);
            }

            return _mapper.Map<LoginDto>(user);
        }
    }
}
