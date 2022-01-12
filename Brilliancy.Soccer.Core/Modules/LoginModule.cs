using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using System;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class LoginModule : BaseModule, ILoginModule
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
                return null;
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if(user == null || !user.IsActive)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
            //return new UserDto
            //{
            //    Login = user.Login,
            //    Id = user.Id
            //};
        }

        public UserDto GetUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null || !user.IsActive)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.Login))
            {
                throw new InvalidDataException("Login nie może być pusty");
            }

            var oldUser = this._dbContext.Users.FirstOrDefault(u => u.Login.ToLower() == dto.Login.ToLower());
            if(oldUser != null)
            {
                throw new InvalidDataException("Wprowadzony login jest już zajęty");
            }

            var user = _mapper.Map<UserDbModel>(dto);
            user.IsActive = true;
            this._dbContext.Users.Add(user);
            this._dbContext.SaveChanges();
        }
    }
}
