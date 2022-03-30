using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.Login;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.Core.Services.EmailSender;
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
        private SoccerDbContext _dbContext { get; }
        private IEmailService _emailService { get; }
        public LoginModule(IMapper mapper, IEmailService emailService, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
            _emailService = emailService;
        }

        public UserDto GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new InvalidDataException(CoreTranslations.Login_EmptyLogin);
            }

            var user = _dbContext.Users
                .Include(u=> u.TournamentAdmins)
                .Include(u => u.OwnedTournaments).FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null || !user.IsActive)
            {
                throw new UserDataException(CoreTranslations.Login_NoUser);
            }

            var result = _mapper.Map<UserDto>(user);
            result.TournamentAdmins = user.TournamentAdmins.Select(t => t.Id).ToList();
            result.TournamentAdmins.AddRange(user.OwnedTournaments.Select(o => o.Id));
            return result;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Login))
            {
                throw new InvalidDataException(CoreTranslations.Login_EmptyLogin);
            }

            var oldUser = this._dbContext.Users.FirstOrDefault(u => u.Login.ToLower() == dto.Login.ToLower());
            if (oldUser != null)
            {
                throw new UserDataException(CoreTranslations.Login_LoginInUse);
            }

            var user = _mapper.Map<UserDbModel>(dto);
            user.IsActive = true;
            this._dbContext.Users.Add(user);
            _emailService.AddWelcomeEmail(user.Email, user.FirstName, GlobalUrlHelper.AppUrl, LanguageHelper.GetLanguage(CoreTranslations.Culture?.TwoLetterISOLanguageName));
            this._dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
        }

        public LoginDto GetUserForUserManager(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new InvalidDataException(CoreTranslations.Login_EmptyLogin);
            }

            var user = _dbContext.Users.Include(u => u.UserRoles).FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
            {
                throw new UserDataException(CoreTranslations.Login_NoUser);
            }

            return _mapper.Map<LoginDto>(user);
        }

        public void ChangePassword(string password, int userId, int? authId = null)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == userId);
            if (user == null)
            {
                throw new UserDataException(CoreTranslations.Login_NoUser);
            }

            if (authId.HasValue)
            {
                var auth = _dbContext.Authentications.FirstOrDefault(a => a.Id == authId.Value);
                if (auth == null)
                {
                    throw new UserDataException(CoreTranslations.Login_NoAuth);
                }
                auth.ConfirmDate = DateTime.Now;
            }
            user.Password = password;
            _dbContext.SaveChanges();
        }
    }
}
