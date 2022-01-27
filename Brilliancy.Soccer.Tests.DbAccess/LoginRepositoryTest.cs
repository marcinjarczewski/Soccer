using Brilliancy.Soccer.DbAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Brilliancy.Soccer.DbAccess.Tests
{
    [TestFixture]
    public class LoginRepositoryTest
    {
        private LoginRepository _loginRepository { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SoccerDbContext>()
                  .UseInMemoryDatabase(databaseName: "SoccerDatabase")
                  .Options;

            // Insert seed data into the database using one instance of the context
            var soccerDbContext = new SoccerDbContext(options);
            soccerDbContext.Users.Add(
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
            soccerDbContext.SaveChanges();
            _loginRepository = new LoginRepository(soccerDbContext);
        }

        [Test]
        public void GetUser_ReturnsUser()
        {
            var result = _loginRepository.GetUser("bigJohn");
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void GetUser_CheckNullInput()
        {
            var result = _loginRepository.GetUser(null);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void GetUser_ReturnsUserCaseSensitivity()
        {
            var result = _loginRepository.GetUser("BIGJOHN");
            Assert.AreNotEqual(null, result);
        }
    }
}