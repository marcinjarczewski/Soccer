using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using Brilliancy.Soccer.DbModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliancy.Soccer.DbAccess.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private SoccerDbContext _dbContext { get; }
        public LoginRepository(SoccerDbContext context) 
        {
            _dbContext = context;
        }

        public LoginDto GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return null;
            }

            var user = _dbContext.Users.Include(u => u.UserRoles).FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
            {
                return null;
            }

            return new LoginDto
            {
                Login = user.Login,
                IsActive = user.IsActive,
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Roles = user.UserRoles.Select(u => new RoleDto { Id = u.RoleId, Name = ((RoleEnum)u.RoleId).ToString()}).ToList()
            };
        }
    }
}
