using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Helpers;
using Brilliancy.Soccer.Core.Services;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class EmailModule : BaseModule, IEmailModule, IEmailRepository
    {
        private SoccerDbContext _dbContext { get; }
        public EmailModule(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }

        public void SentWelcomeEmail(string emailAdrress, string name, LanguageEnum language)
        {
            var template = _dbContext.Templates.Include(t => t.TranslateContent.TranslationEntries).Include(t => t.TranslateHeader)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.UserRegister);
            if(template == null)
            {
                throw new Exception();
            }
            var email = new EmailDbModel
            {
                AddedDate = DateTime.Now,
                Address = emailAdrress,
                Body = language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                Counter = 0,
                Recipient = name,
                Subject = language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content)
            };
            _dbContext.Emails.Add(email);
            _dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
        }

        public IList<EmailDto> GetEmailsToSend(int maxCounter)
        {
            return _mapper.Map<List<EmailDto>>(_dbContext.Emails.Where(e => e.DateSent == null && e.Counter <= maxCounter).ToList());
        }

        public EmailDto Update(EmailDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
