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
    public class PlayerModuleTest
    {
        private IPlayerModule _playerModule;
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
                   Password = "xyz",
                   Players = new System.Collections.Generic.List<DbModels.PlayerDbModel>
                   {
                        new DbModels.PlayerDbModel
                       {
                           Id = 3,
                           NickName = "Rasiak",
                           IsActive = true
                       }
                   }
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
                   Password = "xyz",
                   Players = new System.Collections.Generic.List<DbModels.PlayerDbModel>
                   {
                       new DbModels.PlayerDbModel
                       {
                           Id = 1,
                           NickName = "Messi",
                           IsActive = true
                       },
                        new DbModels.PlayerDbModel
                       {
                           Id = 2,
                           NickName = "Ronaldo",
                           IsActive = true
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
                    Admins = _soccerDbContext.Users.Where(u => u.Id == 2).ToList(),
                    Players = _soccerDbContext.Players.Where(p => p.Id == 3 || p.Id == 2).ToList()
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
                    Admins = _soccerDbContext.Users.Where(u => u.Id == 2).ToList(),
                    Players = _soccerDbContext.Players.Where(p => p.Id == 3).ToList()
                });
            _soccerDbContext.Tournaments.Add(
               new DbModels.TournamentDbModel
               {
                   Address = "adr",
                   DefaultDayOfTheWeek = (int)DaysOfTheWeekEnum.Monday,
                   DefaultHour = new System.TimeSpan(20, 0, 0),
                   Id = 5,
                   IsActive = true,
                   Name = "TTT",
                   Owner = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1),
                   OwnerId = _soccerDbContext.Users.FirstOrDefault(u => u.Id == 1).Id,
                   Admins = _soccerDbContext.Users.Where(u => u.Id == 2).ToList(),
                   Players = _soccerDbContext.Players.Where(p => p.Id == 3 || p.Id == 2).ToList()
               });
            _soccerDbContext.SaveChanges();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();

            _playerModule = new PlayerModule(_mapper, _soccerDbContext);
        }

        [Test]
        public void AddTournamentPlayer_Success()
        {
            var count = _soccerDbContext.Players.Count(p => p.Tournament.Id == 1);
            _playerModule.AddTournamentPlayer(new Common.Dtos.Player.NewPlayerDto
            {
                NickName = "Messi"
            }, 1, 1);
            Assert.AreEqual(count + 1, _soccerDbContext.Players.Count(p => p.Tournament.Id == 1));
        }

        [Test]
        public void AddTournamentPlayer_NoTournament()
        {
            var ex = Assert.Throws<UserDataException>(() => _playerModule.AddTournamentPlayer(new Common.Dtos.Player.NewPlayerDto
            {
                NickName = "Messi"
            }, 0, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoTournament);
        }

        [Test]
        public void AddTournamentPlayer_NoPlayer()
        {
            var ex = Assert.Throws<UserDataException>(() => _playerModule.AddTournamentPlayer(null, 1, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPlayer);
        }

        [Test]
        public void AddTournamentPlayer_EmptyPlayer()
        {
            var ex = Assert.Throws<UserDataException>(() => _playerModule.AddTournamentPlayer(new Common.Dtos.Player.NewPlayerDto(), 1, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPlayer);
        }

        [Test]
        public void RemovePlayerFromTournament_Success()
        {
            var oldIsActive = _soccerDbContext.Players.First(p => p.Id == 1).IsActive;
            _playerModule.RemovePlayerFromTournament(3, 1);
            Assert.IsTrue(oldIsActive && !_soccerDbContext.Players.First(p => p.Id == 3).IsActive);
        }

        [Test]
        public void RemovePlayerFromTournament_Null()
        {
            var ex = Assert.Throws<UserDataException>(() => _playerModule.RemovePlayerFromTournament(0, 2));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPlayer);
        }

        [Test]
        public void RemovePlayerFromTournament_NoAccess()
        {
            var ex = Assert.Throws<UserDataException>(() => _playerModule.RemovePlayerFromTournament(3, 3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }

        [Test]
        public void EditPlayers_AddPlayers()
        {
            var players = _soccerDbContext.Players.Count(p => p.TournamentId == 1);
            _playerModule.EditPlayers(new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>
            {
                new Common.Dtos.Player.PlayerDto
                {
                    FirstName = "Johny",
                    LastName = "Kowalsky",
                    IsActive = true,
                    NickName = "Big papa",
                },
                     new Common.Dtos.Player.PlayerDto
                {
                    FirstName = "Andrew",
                    LastName = "Kowalsky",
                    IsActive = true,
                    NickName = "Small papa",
                }
            }, 1, 1);
            Assert.AreEqual(players + 2, _soccerDbContext.Players.Count(p => p.TournamentId == 1));
        }

        [Test]
        public void EditPlayers_EmptyName()
        {
            var players = _soccerDbContext.Players.Count(p => p.TournamentId == 1);

            var ex = Assert.Throws<UserDataException>(() => _playerModule.EditPlayers(new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>
            {
                new Common.Dtos.Player.PlayerDto
                {
                    FirstName = "Johny",
                    LastName = "Kowalsky",
                    IsActive = true,
                    NickName = "Big papa",
                },
                     new Common.Dtos.Player.PlayerDto
                {
                    FirstName = "",
                    LastName = "",
                    IsActive = true,
                    NickName = "",
                }
            }, 1, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPlayer);
        }

        [Test]
        public void EditPlayers_RemoveAllPlayers()
        {
            _playerModule.EditPlayers(new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>(), 5, 1);
            Assert.AreEqual(0, _soccerDbContext.Players.Where(g => g.IsActive).Count(p => p.TournamentId == 5));
        }

        [Test]
        public void EditPlayers_UpdatePlayers()
        {
            var playersCount = _soccerDbContext.Players.Count(p => p.TournamentId == 5);
            var emptyNamePlayers = _soccerDbContext.Players.Where(p => !string.IsNullOrEmpty(p.FirstName)).Count(p => p.TournamentId == 5);
            _playerModule.EditPlayers(new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>
            {
                new Common.Dtos.Player.PlayerDto
                {
                    Id = 2,
                    FirstName = "Johny",
                    LastName = "Kowalsky",
                    IsActive = true,
                    NickName = "Messi",
                },
                new Common.Dtos.Player.PlayerDto
                {
                    Id = 3,
                    FirstName = "Andrew",
                    LastName = "Kowalsky",
                    IsActive = true,
                    NickName = "Ronaldo",
                }
            }, 5, 1);
            Assert.AreEqual(playersCount, _soccerDbContext.Players.Count(p => p.TournamentId == 5));
            Assert.AreEqual(emptyNamePlayers + 2, _soccerDbContext.Players.Where(p => !string.IsNullOrEmpty(p.FirstName)).Count(p => p.TournamentId == 5));
        }
    }
}