using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Services;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Services.EmailSender;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class LoginModuleTest
    {
        private LoginModule _loginModule;
        private SoccerDbContext _soccerDbContext;
        private int _userCount;

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
                   Email = "test@test.com",
                   FirstName = "Patrick",
                   LastName = "Kowalsky",
                   IsActive = false,
                   Login = "smallJohn",
                   Id = 2,
                   Password = "xyz"
               });
            _soccerDbContext.SaveChanges();
            _userCount = _soccerDbContext.Users.Count();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();
            var emailService = new Mock<IEmailService>();
            var emailSenderService = new EmailSenderService(null, null);
            _loginModule = new LoginModule(mapper, emailService.Object,  _soccerDbContext);
        }

        [Test]
        public void GetUser_Success()
        {
            var user = _loginModule.GetUser("bigjohn");
            Assert.IsTrue(user != null);
        }

        [Test]
        public void GetUser_NoUser()
        {
            var ex = Assert.Throws<UserDataException>(() => _loginModule.GetUser("otherUser"));
            Assert.Pass();
        }

        [Test]
        public void GetUser_NullLogin()
        {
            var ex = Assert.Throws<Common.Exceptions.InvalidDataException>(() => _loginModule.GetUser(null));
            Assert.Pass();
        }

        [Test]
        public void GetUser_EmptyLogin()
        {
            var ex = Assert.Throws<Common.Exceptions.InvalidDataException>(() => _loginModule.GetUser(""));
            Assert.Pass();
        }

        [Test]
        public void RegisterUser_Success()
        {
            var dto = new RegisterUserDto
            {
                Login = "newLogin"
            };
            _loginModule.RegisterUser(dto);
            Assert.AreEqual(_userCount + 1, _soccerDbContext.Users.Count());
        }

        [Test]
        public void RegisterUser_EmptyLogin()
        {
            var dto = new RegisterUserDto
            {
                Login = ""
            };

            var ex = Assert.Throws<Common.Exceptions.InvalidDataException>(() => _loginModule.RegisterUser(dto));
            Assert.Pass();
        }

        [Test]
        public void RegisterUser_NullDto()
        {
            var ex = Assert.Throws<Common.Exceptions.InvalidDataException>(() => _loginModule.RegisterUser(null));
            Assert.Pass();
        }

        [Test]
        public void RegisterUser_LoginInUse()
        {
            var dto = new RegisterUserDto
            {
                Login = "smalljohn"
            };

            var ex = Assert.Throws<Common.Exceptions.UserDataException>(() => _loginModule.RegisterUser(dto));
            Assert.Pass();
        }
    }
}