using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Services;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class EmailModuleTest
    {
        private EmailModule _emailModule;
        private SoccerDbContext _soccerDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SoccerDbContext>()
                  .UseInMemoryDatabase(databaseName: "SoccerDatabase")
                  .Options;

            // Insert seed data into the database using one instance of the context
            _soccerDbContext = new SoccerDbContext(options);
            _soccerDbContext.Database.EnsureDeleted();
            _soccerDbContext.Emails.Add(
                new DbModels.EmailDbModel
                {
                    Id = 1,
                    AddedDate = DateTime.Now,
                    Address = "test@gmail.com",
                    Body = "Body",
                    Counter = 0,
                    DateSent = null,
                    LastErrorDate = null,
                    LastErrorMessage = null,
                    Recipient = "johny test",
                    Subject = "Subject"
                });
            _soccerDbContext.Emails.Add(
                new DbModels.EmailDbModel
                {
                    Id = 2,
                    AddedDate = DateTime.Now,
                    Address = "test2@gmail.com",
                    Body = "Body",
                    Counter = 0,
                    DateSent = null,
                    LastErrorDate = null,
                    LastErrorMessage = null,
                    Recipient = "johny test",
                    Subject = "Subject2"
                });
            _soccerDbContext.Emails.Add(
             new DbModels.EmailDbModel
             {
                 Id = 3,
                 AddedDate = DateTime.Now.AddMinutes(-10),
                 Address = "test2@gmail.com",
                 Body = "Body",
                 Counter = 1,
                 DateSent = null,
                 LastErrorDate = DateTime.Now,
                 LastErrorMessage = "Error",
                 Recipient = "johny test",
                 Subject = "Subject2"
             });
            _soccerDbContext.Emails.Add(
             new DbModels.EmailDbModel
             {
                 Id = 4,
                 AddedDate = DateTime.Now.AddMinutes(-10),
                 Address = "test2@gmail.com",
                 Body = "Body",
                 Counter = 0,
                 DateSent = DateTime.Now,
                 LastErrorDate = DateTime.Now.AddMinutes(-5),
                 LastErrorMessage = "Error",
                 Recipient = "johny test",
                 Subject = "Subject2"
             });
            _soccerDbContext.Templates.Add(new DbModels.TemplateDbModel
            {
                Id = (int)TemplateEnum.UserRegister,
                Header = "header",
                Content = "Content"
            });
            _soccerDbContext.SaveChanges();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();
            _emailModule = new EmailModule(mapper, _soccerDbContext);
            var service = new EmailSenderService(null, null);
        }

        [Test]
        public void GetEmailsToSend_Success()
        {
            var emails = _emailModule.GetEmailsToSend(1);
            Assert.AreEqual(3, emails.Count());
        }

        [Test]
        public void SentWelcomeEmail_Success()
        {
            var count = _soccerDbContext.Emails.Count();
            _emailModule.SentWelcomeEmail("test@gmail.com","Johny Kowalsky", "mysite.com", Common.Enums.LanguageEnum.Polish);
            Assert.AreEqual(count + 1, _soccerDbContext.Emails.Count());
        }

        [Test]
        public void Update_Success()
        {
            var count = _soccerDbContext.Emails.Where(e => e.DateSent == null).Count();
            _emailModule.Update(new Common.Dtos.Email.EmailDto
            {
                AddedDate = DateTime.Now,
                Address = "adr@gmail.com",
                Id = 1,
                DateSent = DateTime.Now,
                Counter = 0
            });
            Assert.AreEqual(count - 1, _soccerDbContext.Emails.Where(e => e.DateSent == null).Count());
        }

        [Test]
        public void Update_Null()
        {
            var ex = Assert.Throws<UserDataException>(() => _emailModule.Update(null));
            Assert.IsTrue(ex.Message == CoreTranslations.Email_NoEmail);
        }

        [Test]
        public void Update_NotExists()
        {
            var ex = Assert.Throws<UserDataException>(() => _emailModule.Update(new Common.Dtos.Email.EmailDto
            {
                Id = 6789
            }));
            Assert.IsTrue(ex.Message == CoreTranslations.Email_NoEmail);
        }
    }
}