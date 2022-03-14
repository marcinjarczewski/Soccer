using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Enums;
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
    public class AuthenticationModule : BaseModule, IAuthenticationModule
    {
        private SoccerDbContext _dbContext {get;}
        private IEmailService _emailService { get; }
        private IConfigurationRepository _configurationRepository { get; }
        public AuthenticationModule(IMapper mapper, IEmailService emailService, IConfigurationRepository configurationRepository, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
            _emailService = emailService;
            _configurationRepository = configurationRepository;
        }
        public void InvitePlayer(AuthenticationDto dto, int userId)
        {
            if(dto == null)
            {
                throw new InvalidDataException(CoreTranslations.Authentication_NoEmail);
            }

            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new UserDataException(CoreTranslations.Authentication_NoEmail);
            }
            var player = this._dbContext.Players
                .Include(t => t.Tournament.Admins)
                .FirstOrDefault(u => u.Id == dto.PlayerId);
            if (player == null)
            {
                throw new InvalidDataException(CoreTranslations.Authentication_NoPlayer);
            }
            CheckPrivilages(player.Tournament, userId);

            var daysValid = int.Parse(_configurationRepository.GetValue(ConfigurationDictionary.InvitePlayerDaysExpiration));
            var auth = new AuthenticationDbModel
            {
                CreateDate = DateTime.Now,
                CreatedByUserId = userId,
                Data = $"email:{dto.Email};playerId:{dto.PlayerId}",
                DateValidaty = DateTime.Now.AddDays(daysValid),
                Key = GenerateKey(),
                TypeId = (int)AuthenticationTypeEnum.TournamentPlayerInvite
            };
            _dbContext.Authentications.Add(auth);
            _emailService.AddPlayerInviteEmail(
                dto.Email,
                $"{player.FirstName} {(string.IsNullOrEmpty(player.NickName)? string.Empty: (" '" + player.NickName + "' "))} {player.LastName}",
                player.Tournament.Name,
                auth.Key,
                GlobalUrlHelper.AppUrl,
                LanguageHelper.GetLanguage(CoreTranslations.Culture?.TwoLetterISOLanguageName));
            this._dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
        }

        public void ForgottenPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UserDataException(CoreTranslations.Authentication_NoEmail);
            }
            var users = _dbContext.Users.Where(u => u.IsActive && u.Email.ToLower() == email.ToLower());
            if (users.Count() == 0)
            {
                //protect users from searching email based on response.
                return;
            }
            if (users.Count() > 1)
            {
                throw new UserDataException(CoreTranslations.Authentication_EmailNotUnique);
            }
            var user = users.First();
            var daysValid = int.Parse(_configurationRepository.GetValue(ConfigurationDictionary.ResetPasswordDaysExpiration));
            var auth = new AuthenticationDbModel
            {
                CreateDate = DateTime.Now,
                CreatedByUser = users.FirstOrDefault(),
                Data = $"email:{email}",
                DateValidaty = DateTime.Now.AddDays(daysValid),
                Key = GenerateKey(),
                TypeId = (int)AuthenticationTypeEnum.ResetPassword
            };
            _dbContext.Authentications.Add(auth);
            _emailService.AddForgottenPasswordEmail(
                email,
                $"{user.FirstName} {user.LastName}",
                auth.Key,
                GlobalUrlHelper.AppUrl,
                LanguageHelper.GetLanguage(CoreTranslations.Culture?.TwoLetterISOLanguageName));
            this._dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
        }


        private string GenerateKey()
        {
            string newGuid = "";
            while(string.IsNullOrEmpty(newGuid)|| _dbContext.Authentications.Any(a => a.Key == newGuid))
            {
                newGuid = Guid.NewGuid().ToString();
            }

            return newGuid;
        }

        public void InviteAdmin(AuthenticationDto dto, int userId)
        {
            if (dto == null)
            {
                throw new InvalidDataException(CoreTranslations.Authentication_NoEmail);
            }

            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new UserDataException(CoreTranslations.Authentication_NoEmail);
            }

            var player = this._dbContext.Players
                .Include(t => t.Tournament.Admins)
                .FirstOrDefault(u => u.Id == dto.PlayerId);
            CheckPrivilages(player.Tournament, userId);
            if (player == null)
            {
                throw new InvalidDataException(CoreTranslations.Authentication_NoPlayer);
            }

            if (!player.UserId.HasValue)
            {
                throw new UserDataException(CoreTranslations.Authentication_NoUserForInvite);
            }

            var daysValid = int.Parse(_configurationRepository.GetValue(ConfigurationDictionary.InvitePlayerDaysExpiration));
            var auth = new AuthenticationDbModel
            {
                CreateDate = DateTime.Now,
                CreatedByUserId = userId,
                Data = $"email:{dto.Email};playerId:{dto.PlayerId}",
                DateValidaty = DateTime.Now.AddDays(daysValid),
                Key = GenerateKey(),
                TypeId = (int)AuthenticationTypeEnum.TournamentAdminInvite
            };
            _dbContext.Authentications.Add(auth);
            _emailService.AddPlayerInviteEmail(
                dto.Email,
                $"{player.FirstName} {(string.IsNullOrEmpty(player.NickName) ? string.Empty : (" '" + player.NickName + "' "))} {player.LastName}",
                player.Tournament.Name,
                auth.Key,
                GlobalUrlHelper.AppUrl,
                LanguageHelper.GetLanguage(CoreTranslations.Culture?.TwoLetterISOLanguageName));
            this._dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
        }
    }
}
