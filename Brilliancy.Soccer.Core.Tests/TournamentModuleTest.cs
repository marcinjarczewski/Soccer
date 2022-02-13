using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Automapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class TournamentModuleTest
    {
        private ITournamentModule _tournamentModule;
        private SoccerDbContext _soccerDbContext;
        private IMapper _mapper;

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
            _soccerDbContext.Users.Add(
               new DbModels.UserDbModel
               {
                   Email = "test@test.com",
                   FirstName = "Johny",
                   LastName = "Kowalsky",
                   IsActive = true,
                   Login = "bigJohn2",
                   Id = 3,
                   Password = "xyz"
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
                    OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id
                });
            _soccerDbContext.Tournaments.Add(
                new DbModels.TournamentDbModel
                {
                    Address = "adr",
                    DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                    DefaultHour = new System.TimeSpan(20, 0, 0),
                    Id = 2,
                    IsActive = false,
                    Name = "My tourn",
                    Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1),
                    OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id
                });
            _soccerDbContext.Tournaments.Add(
                new DbModels.TournamentDbModel
                {
                    Address = "adr",
                    DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                    DefaultHour = new System.TimeSpan(20, 0, 0),
                    Id = 3,
                    IsActive = false,
                    Name = "TTT",
                    Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1),
                    OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id,
                    Admins = _soccerDbContext.Users.Where(u => u.Id == 2).ToList()
                });
            _soccerDbContext.Tournaments.Add(
                new DbModels.TournamentDbModel
                {
                    Address = "Test adr",
                    DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                    DefaultHour = new System.TimeSpan(20, 0, 0),
                    Id = 4,
                    IsActive = false,
                    Name = "My other t",
                    Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1),
                    OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id,
                    Admins = _soccerDbContext.Users.Where(u => u.Id == 2).ToList()
                });
            _soccerDbContext.SaveChanges();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();

            _tournamentModule = new TournamentModule(_mapper, _soccerDbContext);
        }

        [Test]
        public void GetTournament_Success()
        {
            var tournament = _tournamentModule.GetTournament(1, 1);
            Assert.IsTrue(tournament != null);
        }

        [Test]
        public void GetTournament_NoTournament()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.GetTournament(0, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoTournament);
        }

        [Test]
        public void GetTournament_InactiveTournament()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.GetTournament(2, 2));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoTournament);
        }

        [Test]
        public void GetTournament_InactiveTournamentAsOwner()
        {
            var tournament = _tournamentModule.GetTournament(2, 1);
            Assert.IsTrue(tournament != null);
        }

        [Test]
        public void GetTournament_InactiveTournamentAsAdmin()
        {
            var tournament = _tournamentModule.GetTournament(3, 2);
            Assert.IsTrue(tournament != null && tournament.Id == 3);
        }

        [Test]
        public void AddTournament_Success()
        {
            int count = _soccerDbContext.Tournaments.Count();
            _tournamentModule.AddTournament(new Common.Dtos.Tournament.NewTournamentDto
            {

                Address = "testAdr",
                DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Friday,
                DefaultHour = new System.TimeSpan(17,0,0),
                Name = "Ttt tt",
                OwnerId = 1,
            });           
            Assert.AreEqual(count + 1, _soccerDbContext.Tournaments.Count());
        }
        [Test]
        public void AddTournament_NoUser()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.AddTournament(new Common.Dtos.Tournament.NewTournamentDto
            {

                Address = "testAdr",
                DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Friday,
                DefaultHour = new System.TimeSpan(17, 0, 0),
                Name = "Ttt tt",
                OwnerId = 4,
            }));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoUser);
        }

        [Test]
        public void DeleteTournament_Success()
        {
            int count = _soccerDbContext.Tournaments.Where(t => t.IsActive).Count();
            _tournamentModule.DeleteTournament(1,1);
            Assert.AreEqual(count - 1, _soccerDbContext.Tournaments.Where(t => t.IsActive).Count());
        }

        [Test]
        public void DeleteTournament_NoPrivilages()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.DeleteTournament(1,3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void ActivateTournament_Success()
        {
            int count = _soccerDbContext.Tournaments.Where(t => t.IsActive).Count();
            _tournamentModule.ActivateTournament(2, 1);
            Assert.AreEqual(count + 1, _soccerDbContext.Tournaments.Where(t => t.IsActive).Count());
        }

        [Test]
        public void ActivateTournament_NoPrivilages()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.ActivateTournament(2, 3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void EditTournament_Success()
        {
            _tournamentModule.EditTournament(new Common.Dtos.Tournament.NewTournamentDto
            {
                Id = 1,
                Address = "testAdr",
                DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Friday,
                DefaultHour = new System.TimeSpan(17, 0, 0),
                Name = "Ttt tt",
                OwnerId = 1,
            },1);
            Assert.Pass();
        }

        [Test]
        public void EditTournament_NoPrivilages()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.EditTournament(new Common.Dtos.Tournament.NewTournamentDto
            {
                Id = 1,
                Address = "testAdr",
                DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Friday,
                DefaultHour = new System.TimeSpan(17, 0, 0),
                Name = "Ttt tt",
                OwnerId = 1,
            }, 3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void EditTournament_Null()
        {
            var ex = Assert.Throws<UserDataException>(() => _tournamentModule.EditTournament(null, 3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoTournament);
        }

        [Test]
        public void GetTournaments_PagingMaxCap()
        {
           int perPage = 2;
           var paged =  _tournamentModule.GetTournaments("", 2, perPage);
           Assert.AreEqual(_soccerDbContext.Tournaments.Count(), paged.TotalItemCount);
        }

        [Test]
        public void GetTournaments_FilterName()
        {
            int perPage = 5;
            var paged = _tournamentModule.GetTournaments("My tourn", 2, perPage);
            Assert.AreEqual(2, paged.TotalItemCount);
        }

        [Test]
        public void GetTournaments_FilterNameCaseSense()
        {
            int perPage = 5;
            int totalPages = 1;
            var paged = _tournamentModule.GetTournaments("OTHER", 1, perPage);
            Assert.AreEqual(totalPages, paged.Count);
        }
    }
}