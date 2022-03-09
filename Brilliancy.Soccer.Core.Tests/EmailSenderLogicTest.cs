using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services.EmailSender;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Brilliancy.Soccer.Core.Tests
{
    public class EmailSenderLogicTest
    {
        private SoccerDbContext _soccerDbContext;
        private IMapper _mapper;
        private IConfigurationRepository _mockedConfig;

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

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();

            var configMock = new Mock<IConfigurationRepository>();
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailServiceSendingTime)).Returns("15");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailServiceSleepTime)).Returns("25");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterName)).Returns("Johny Kowalsky");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterPassword)).Returns("JoKo");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterAddress)).Returns("123@gmail.com");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterSMTP)).Returns("smtp.test.en");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterPort)).Returns("567");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterSSLEnabled)).Returns("true");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailRegisterReplyTo)).Returns("replyto@gmail.com");
            _mockedConfig = configMock.Object;
        }

        [Test]
        public void Send_Success()
        {
            var emailMock = new Mock<IEmailRepository>();
            emailMock.Setup(c => c.GetEmailsToSend(It.IsAny<int>()))
                .Returns(_mapper.Map<List<Common.Dtos.Email.EmailDto>>(_soccerDbContext.Emails.Where(e => e.DateSent == null).ToList()));
            var emailCreatorMock = new Mock<IEmailCreator>();
            emailCreatorMock.Setup(c => c.CreateMessage(It.IsAny<Common.Dtos.Email.EmailDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new MailMessage());
            var smtpMock = new Mock<ISmtpClient>();
            var logic = new EmailSenderLogic(emailMock.Object, _mockedConfig, smtpMock.Object, emailCreatorMock.Object);

            var sleepTime = logic.Send(logic.GetEmails());
            Assert.AreEqual(25, sleepTime);
        }


        [Test]
        public void Send_UpdateEmail()
        {
            var emailMock = new Mock<IEmailRepository>();
            var emailDto = new Common.Dtos.Email.EmailDto
            {
                AddedDate = DateTime.Now,
                Counter = 0,
                LastErrorDate = null,
                Address = "",
                LastErrorMessage = null
            };
            emailMock.Setup(c => c.GetEmailsToSend(It.IsAny<int>()))
                .Returns(new List<Common.Dtos.Email.EmailDto>
                {
                  emailDto
                });
            var emailCreatorMock = new Mock<IEmailCreator>();
            emailCreatorMock.Setup(c => c.CreateMessage(It.IsAny<Common.Dtos.Email.EmailDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new MailMessage());
            var smtpMock = new Mock<ISmtpClient>();
            var logic = new EmailSenderLogic(emailMock.Object, _mockedConfig, smtpMock.Object, emailCreatorMock.Object);

            var sleepTime = logic.Send(logic.GetEmails());
            Assert.IsNotNull(emailDto.DateSent);
        }


        [Test]
        public void Send_UpdateEmailError()
        {
            var emailMock = new Mock<IEmailRepository>();
            var emailDto = new Common.Dtos.Email.EmailDto
            {
                AddedDate = DateTime.Now,
                Counter = 0,
                LastErrorDate = null,
                Address = "",
                LastErrorMessage = null
            };
            emailMock.Setup(c => c.GetEmailsToSend(It.IsAny<int>()))
                .Returns(new List<Common.Dtos.Email.EmailDto>
                {
                  emailDto
                });
            var emailCreatorMock = new Mock<IEmailCreator>();
            emailCreatorMock.Setup(c => c.CreateMessage(It.IsAny<Common.Dtos.Email.EmailDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new NullReferenceException("error"));
            var smtpMock = new Mock<ISmtpClient>();
            var logic = new EmailSenderLogic(emailMock.Object, _mockedConfig, smtpMock.Object, emailCreatorMock.Object);

            var sleepTime = logic.Send(logic.GetEmails());
            Assert.IsNotNull(emailDto.LastErrorDate);
            Assert.IsNotNull(emailDto.LastErrorMessage);
            Assert.AreEqual(1, emailDto.Counter);
        }

        [Test]
        public void GetTimeTable_Success()
        {
            var configMock = new Mock<IConfigurationRepository>();
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailServiceSendingTime)).Returns("10;20;30;60;120");
            var logic = new EmailSenderLogic(null, configMock.Object, null, null);
            var table = logic.GetTimeTable();
            Assert.AreEqual(5, table.Count());
        }

        [Test]
        public void GetTimeTable_WrongConfig()
        {
            var configMock = new Mock<IConfigurationRepository>();
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.EmailServiceSendingTime)).Returns("10;20;30;60-120");
            var logic = new EmailSenderLogic(null, configMock.Object, null, null);
            var ex = Assert.Throws<FormatException>(() => { logic.GetTimeTable(); });
        }

        [Test]
        public void IsSendingTime_Success()
        {
            var logic = new EmailSenderLogic(null, null, null, null);
            Assert.IsTrue(logic.IsSendingTime(new List<int> { 1, 2 }, 0, DateTime.Now));
        }

        [Test]
        public void IsSendingTime_NoTimeTableSuccess()
        {
            var logic = new EmailSenderLogic(null, null, null, null);
            Assert.IsTrue(logic.IsSendingTime(null, 0, DateTime.Now));
        }

        [Test]
        public void IsSendingTime_NoTimeTableFailed()
        {
            var logic = new EmailSenderLogic(null, null, null, null);
            Assert.IsFalse(logic.IsSendingTime(null, 1, DateTime.Now));
        }

        [Test]
        public void IsSendingTime_Wait5MinutesFailed()
        {
            var logic = new EmailSenderLogic(null, null, null, null);
            Assert.IsFalse(logic.IsSendingTime(new List<int> { 5 }, 1, DateTime.Now));
        }

        [Test]
        public void IsSendingTime_Wait5MinutesSuccess()
        {
            var logic = new EmailSenderLogic(null, null, null, null);
            Assert.IsTrue(logic.IsSendingTime(new List<int> { 5 }, 1, DateTime.Now.AddMinutes(-10)));
        }
    }
}