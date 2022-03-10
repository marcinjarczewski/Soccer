﻿using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.Core.Services.EmailSender;
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

        public void SentWelcomeEmail(string emailAdrress, string name, string appUrl, LanguageEnum language)
        {
            var template = _dbContext.Templates.Include(t => t.TranslateContent.TranslationEntries).Include(t => t.TranslateHeader)
                .FirstOrDefault(f => f.Id == (int)TemplateEnum.UserRegister);
            if(template == null)
            {
                throw new  UserDataException(CoreTranslations.Email_NoEmail);
            }
            var body = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateContent?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content),
                    new { Name = name, AppUrl = appUrl });
            var subject = TemplateFillerHelper.FillTemplate(language == LanguageEnum.Polish ?
                    template.Content :
                    (template.TranslateHeader?.TranslationEntries?.FirstOrDefault(te => te.LanguageId == (int)language)?.Text ?? template.Content)
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
            _dbContext.SaveChanges();
            var emailService = EmailSenderService.GetInstance();
            emailService.WakeUp();
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
