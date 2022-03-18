using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Services
{
    public class EmailService : BaseModule, IEmailService, IEmailRepository
    {
        private SoccerDbContext _dbContext { get; }
        public EmailService(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }
        /// <summary>
        /// Email is added as a part of a transaction. Call save chages manually after using it.
        /// </summary>
        /// <param name="emailAdrress"></param> 
        /// <param name="name"></param>
        /// <param name="appUrl"></param>
        /// <param name="language"></param>
        public void AddWelcomeEmail(string emailAdrress, string name, string appUrl, LanguageEnum language)
        {
            var template = _dbContext.Templates
                .Include(t => t.TranslateContent.TranslationEntries)
                .Include(t => t.TranslateHeader.TranslationEntries)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.UserRegister);
            if(template == null)
            {
                throw new  UserDataException(CoreTranslations.Email_NoTemplate);
            }
            var body = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                    new { Name = name, AppUrl = appUrl });
            var subject = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Header :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Header)
                    , new { Name = name });
            var email = new EmailDbModel
            {
                AddedDate = DateTime.Now,
                Address = emailAdrress,
                Body = body,
                Counter = 0,
                Recipient = name,
                Subject = subject
            };
            _dbContext.Emails.Add(email);
        }

        /// <summary>
        /// Email is added as a part of a transaction. Call save chages manually after using it.
        /// </summary>
        /// <param name="emailAdrress"></param> 
        /// <param name="name"></param>
        /// <param name="appUrl"></param>
        /// <param name="language"></param>
        public void AddPlayerInviteEmail(string emailAdrress, string name, string tournamentName, string key, string appUrl, LanguageEnum language)
        {
            var template = _dbContext.Templates
                .Include(t => t.TranslateContent.TranslationEntries)
                .Include(t => t.TranslateHeader.TranslationEntries)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.PlayerInvite);
            if (template == null)
            {
                throw new UserDataException(CoreTranslations.Email_NoTemplate);
            }
            var body = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                    new { 
                        Name = name,
                        AppUrl = appUrl,
                        TournamentName = tournamentName,
                        AppUrlKey = $"{appUrl}/InvitePlayers/{key}",
                        AppUrlRegister = $"{appUrl}/login"
                    });
            var subject = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Header :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Header)
                    , new { Name = name });
            var email = new EmailDbModel
            {
                AddedDate = DateTime.Now,
                Address = emailAdrress,
                Body = body,
                Counter = 0,
                Recipient = name,
                Subject = subject
            };
            _dbContext.Emails.Add(email);
        }

        public void AddForgottenPasswordEmail(string emailAdrress, string name, string key, string appUrl, LanguageEnum language)
        {
            var template = _dbContext.Templates
                .Include(t => t.TranslateContent.TranslationEntries)
                .Include(t => t.TranslateHeader.TranslationEntries)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.ForgottenPassword);
            if (template == null)
            {
                throw new UserDataException(CoreTranslations.Email_NoTemplate);
            }
            var body = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                    new
                    {
                        Name = name,
                        AppUrl = appUrl,
                        AppUrlKey = $"{appUrl}/LostPassword/{key}"
                    });
            var subject = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Header :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Header)
                    , new { Name = name });
            var email = new EmailDbModel
            {
                AddedDate = DateTime.Now,
                Address = emailAdrress,
                Body = body,
                Counter = 0,
                Recipient = name,
                Subject = subject
            };
            _dbContext.Emails.Add(email);
        }

        /// <summary>
        /// Email is added as a part of a transaction. Call save chages manually after using it.
        /// </summary>
        /// <param name="emailAdrress"></param> 
        /// <param name="name"></param>
        /// <param name="appUrl"></param>
        /// <param name="language"></param>
        public void AddAdminInviteEmail(string emailAdrress, string name, string tournamentName, string key, string appUrl, LanguageEnum language)
        {
            var template = _dbContext.Templates
                .Include(t => t.TranslateContent.TranslationEntries)
                .Include(t => t.TranslateHeader.TranslationEntries)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.AdminInvite);
            if (template == null)
            {
                throw new UserDataException(CoreTranslations.Email_NoTemplate);
            }
            var body = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                    new
                    {
                        Name = name,
                        AppUrl = appUrl,
                        TournamentName = tournamentName,
                        AppUrlKey = $"{appUrl}/InviteAdmin/{key}",
                        AppUrlRegister = $"{appUrl}/login"
                    });
            var subject = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Header :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Header)
                    , new { Name = name });
            var email = new EmailDbModel
            {
                AddedDate = DateTime.Now,
                Address = emailAdrress,
                Body = body,
                Counter = 0,
                Recipient = name,
                Subject = subject
            };
            _dbContext.Emails.Add(email);
        }

        public List<EmailDto> GetEmailsToSend(int maxCounter)
        {
            return _mapper.Map<List<EmailDto>>(_dbContext.Emails.Where(e => e.DateSent == null && e.Counter <= maxCounter).ToList());
        }

        public void Update(EmailDto dto)
        {
            if(dto == null)
            {
                throw new UserDataException(CoreTranslations.Email_NoEmail);
            }
            var email = _dbContext.Emails.FirstOrDefault(e => e.Id == dto.Id);
            if (email == null)
            {
                throw new UserDataException(CoreTranslations.Email_NoEmail);
            }
            email.Counter = dto.Counter;
            email.DateSent = dto.DateSent;
            email.LastErrorDate = dto.LastErrorDate;
            email.LastErrorMessage = dto.LastErrorMessage;
            _dbContext.Emails.Update(email);
            _dbContext.SaveChanges();
        }
    }
}
