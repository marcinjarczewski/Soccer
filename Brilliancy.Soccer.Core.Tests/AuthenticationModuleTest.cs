using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Repositories;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class AuthenticationModuleTest
    {
        private AuthenticationModule _authenticationModule;
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
            _soccerDbContext.Users.Add(
               new DbModels.UserDbModel
               {
                   Email = "test@test.com",
                   FirstName = "Johny",
                   LastName = "Kowalsky",
                   IsActive = true,
                   Login = "bigJohn",
                   Id = 1,
                   Password = "xyz"
               });
            _soccerDbContext.Users.Add(
                new DbModels.UserDbModel
                {
                    Email = "test@test2.com",
                    FirstName = "Adam",
                    LastName = "Nowaq",
                    IsActive = true,
                    Login = "smallJohn",
                    Id = 2,
                });
            _soccerDbContext.SaveChanges();
            _soccerDbContext.Tournaments.Add(
            new DbModels.TournamentDbModel
            {
                Address = "Test adr",
                DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                DefaultHour = new System.TimeSpan(20, 0, 0),
                Id = 1,
                IsActive = true,
                Name = "My tourn",
                Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1),
                OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id,
                Players = new System.Collections.Generic.List<DbModels.PlayerDbModel>
                {
                    new DbModels.PlayerDbModel
                    {
                        Id = 1,
                        FirstName = "Johny",
                        LastName = "Sprinter",
                        UserId = 2
                    },
                    new DbModels.PlayerDbModel
                    {
                        Id = 3,
                        FirstName = "Robert",
                        LastName = "Lewandowski",
                    }
                }
            });
            _soccerDbContext.SaveChanges();
            _soccerDbContext.Tournaments.Add(
                new DbModels.TournamentDbModel
                {
                    Address = "Test adr",
                    DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                    DefaultHour = new System.TimeSpan(20, 0, 0),
                    Id = 2,
                    IsActive = true,
                    Name = "My tourn",
                    Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 2),
                    OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 2).Id,
                    Players = new System.Collections.Generic.List<DbModels.PlayerDbModel>
                    {
                        new DbModels.PlayerDbModel
                        {
                            Id = 2,
                            FirstName = "Johny",
                            LastName = "Test"
                        }
                    }
                });
            _soccerDbContext.SaveChanges();
     
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();
            var emailService = new Mock<IEmailService>();
            var emailServiceSender = new EmailSenderService(null, null);
            var configMock = new Mock<IConfigurationRepository>();
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.InvitePlayerDaysExpiration)).Returns("15");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.InviteAdminDaysExpiration)).Returns("15");
            configMock.Setup(c => c.GetValue(ConfigurationDictionary.ResetPasswordDaysExpiration)).Returns("5");
            _authenticationModule = new AuthenticationModule(mapper, emailService.Object, configMock.Object, _soccerDbContext);
        }

        [Test]
        public void InvitePlayer_Success()
        {
            var count = _soccerDbContext.Authentications.Count();
            _authenticationModule.InvitePlayer(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 1,
            }, 1);
            Assert.AreEqual(count + 1, _soccerDbContext.Authentications.Count());
        }

        [Test]
        public void ForgottenPassword_Success()
        {
            var count = _soccerDbContext.Authentications.Count();
            _authenticationModule.ForgottenPassword("test@test.com");
            Assert.AreEqual(count + 1, _soccerDbContext.Authentications.Count());
        }

        [Test]
        public void ForgottenPassword_EmptyEmail()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.ForgottenPassword(""));
            Assert.IsTrue(ex.Message == CoreTranslations.Authentication_NoEmail);
        }

        [Test]
        public void InvitePlayer_NoUserPrivilages()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.InvitePlayer(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 1,
            }, 2));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void InvitePlayer_PlayerNotFromTournament()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.InvitePlayer(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 2,
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }


        [Test]
        public void InviteAdmin_Success()
        {
            var count = _soccerDbContext.Authentications.Count();
            _authenticationModule.InviteAdmin(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 1,
            }, 1);
            Assert.AreEqual(count + 1, _soccerDbContext.Authentications.Count());
        }

        [Test]
        public void InviteAdmin_NoUserPrivilages()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.InviteAdmin(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 1,
            }, 2));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void InviteAdmin_NoUserForInvite()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.InviteAdmin(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 3,
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Authentication_NoUserForInvite);
        }

        [Test]
        public void InviteAdmin_PlayerNotFromTournament()
        {
            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _authenticationModule.InviteAdmin(new Common.Dtos.Authentication.AuthenticationDto
            {
                Email = "test@tt.com",
                PlayerId = 2,
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }
    }
}