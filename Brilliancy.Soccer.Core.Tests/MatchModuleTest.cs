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
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Brilliancy.Soccer.Core.Tests
{
    public class MatchModuleTest
    {
        private IMatchModule _matchModule;
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
               });
            _soccerDbContext.MatchStates.Add(new DbModels.MatchStateDbModel
            {
                Id = (int)MatchStateEnum.Creating,
                Name = MatchStateEnum.Creating.ToString()
            });
            _soccerDbContext.MatchStates.Add(new DbModels.MatchStateDbModel
            {
                Id = (int)MatchStateEnum.Ongoing,
                Name = MatchStateEnum.Ongoing.ToString()
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
                    Players = new System.Collections.Generic.List<DbModels.PlayerDbModel>
                   {
                       new DbModels.PlayerDbModel
                       {
                           Id = 3,
                           NickName = "Rasiak",
                           IsActive = true
                       },
                        new DbModels.PlayerDbModel
                       {
                           Id = 4,
                           NickName = "Lewy",
                           IsActive = true
                       },
                        new DbModels.PlayerDbModel
                       {
                           Id = 5,
                           NickName = "Matty",
                           IsActive = true
                       },
                        new DbModels.PlayerDbModel
                       {
                           Id = 6,
                           NickName = "Krycha",
                           IsActive = true
                       }
                   }
                });
            _soccerDbContext.SaveChanges();
            _soccerDbContext.Teams.Add(
               new DbModels.TeamDbModel
               {
                   Id = 1,
                   IsActive = true,
                   Name = "Abc",
                   TournamentId = 1
               });
            _soccerDbContext.Teams.Add(
              new DbModels.TeamDbModel
              {
                  Id = 2,
                  IsActive = true,
                  Name = "Abcd",
                  TournamentId = 1,
              });
            _soccerDbContext.Teams.Add(
             new DbModels.TeamDbModel
             {
                 Id = 3,
                 IsActive = false,
                 Name = "Abcd",
                 TournamentId = 1
             });
            _soccerDbContext.Teams.Add(
             new DbModels.TeamDbModel
             {
                 Id = 4,
                 IsActive = true,
                 Name = "team 4",
                 TournamentId = 4,
             });
            _soccerDbContext.Teams.Add(
               new DbModels.TeamDbModel
               {
                   Id = 5,
                   IsActive = true,
                   Name = "team 5",
                   TournamentId = 4,
                   Players = _soccerDbContext.Players.Where(p => p.Id == 3 || p.Id == 4).ToList()
               });
            _soccerDbContext.SaveChanges();
            _soccerDbContext.Matches.Add(
             new DbModels.MatchDbModel
             {
                 Id = 1,
                 IsActive = true,
                 TournamentId = 3,
             });
            _soccerDbContext.Matches.Add(
                new DbModels.MatchDbModel
                {
                    Id = 2,
                    IsActive = true,
                    TournamentId = 4,
                    StateId = (int)MatchStateEnum.Ongoing,
                    Goals = new System.Collections.Generic.List<DbModels.GoalDbModel>
                    {
                        new DbModels.GoalDbModel
                        {
                            Id = 1,
                            ScorerId = 3,
                            AssistId = 4,
                            IsActive = true
                        },
                          new DbModels.GoalDbModel
                        {
                            Id = 2,
                            ScorerId = 4,
                            AssistId = 3,
                            IsActive = true
                        }
                    },
                    HomeTeam = _soccerDbContext.Teams.FirstOrDefault(t => t.Id == 4),
                    AwayTeam = _soccerDbContext.Teams.FirstOrDefault(t => t.Id == 5),
                });
            _soccerDbContext.SaveChanges();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperCommonProfile>();
                cfg.AddProfile<AutomapperCoreProfile>();
            }).CreateMapper();

            var logger = Mock.Of<ILogger<MatchModule>>();
            _matchModule = new MatchModule(_mapper, logger, _soccerDbContext);
        }

        [Test]
        public void AddTournamentMatch_Success()
        {
            var count = _soccerDbContext.Matches.Count(p => p.Tournament.Id == 1);
            _matchModule.AddTournamentMatch(new Common.Dtos.Match.NewMatchDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                TournamentId = 1
            }, 1);
            Assert.AreEqual(count + 1, _soccerDbContext.Matches.Count(p => p.Tournament.Id == 1));
        }

        [Test]
        public void AddTournamentMatch_NoPrivilages()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddTournamentMatch(new Common.Dtos.Match.NewMatchDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                TournamentId = 1
            }, 3));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoPrivileges);
        }


        [Test]
        public void AddTournamentMatch_Null()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddTournamentMatch(null, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoMatch);
        }

        [Test]
        public void AddTournamentMatch_NoHomeTeam()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddTournamentMatch(new Common.Dtos.Match.NewMatchDto
            {
                HomeTeamName = "",
                AwayTeamName = "Team B",
                TournamentId = 1
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoHomeTeam);
        }

        [Test]
        public void AddTournamentMatch_NoAwayTeam()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddTournamentMatch(new Common.Dtos.Match.NewMatchDto
            {
                HomeTeamName = "Team A",
                TournamentId = 1
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_NoAwayTeam);
        }

        [Test]
        public void AddTournamentMatch_SameTeam()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddTournamentMatch(new Common.Dtos.Match.NewMatchDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team A",
                TournamentId = 1
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_SameTeams);
        }

        [Test]
        public void EditCreatingMatch_ChangeNames()
        {
            var matchDto = new Common.Dtos.Match.MatchCreatingEditDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                Id = 2,
            };
            _matchModule.EditCreatingMatch(matchDto, 1);
            var homeTeam = _soccerDbContext.Teams.FirstOrDefault(t => t.Id == 4);
            var awayTeam = _soccerDbContext.Teams.FirstOrDefault(t => t.Id == 5);
            Assert.AreSame("Team A", homeTeam.Name);
            Assert.AreSame("Team B", awayTeam.Name);
        }
        [Test]
        public void EditCreatingMatch_AddPlayers()
        {
            var matchDto = new Common.Dtos.Match.MatchCreatingEditDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                Id = 2,
                HomePlayers = new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>
                {
                    new Common.Dtos.Player.PlayerDto
                    {
                        Id = 5
                    },
                    new Common.Dtos.Player.PlayerDto
                    {
                        Id = 6
                    }
                }
            };
            _matchModule.EditCreatingMatch(matchDto, 1);
            var match = _soccerDbContext.Matches
                .Include(m => m.HomeTeam.Players).Include(m => m.AwayTeam.Players)
                .FirstOrDefault(m => m.Id == 2);
            Assert.AreEqual(2, match.HomeTeam.Players.Count);
        }

        [Test]
        public void EditCreatingMatch_NoPlayerInTournament()
        {
            var matchDto = new Common.Dtos.Match.MatchCreatingEditDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                Id = 2,
                HomePlayers = new System.Collections.Generic.List<Common.Dtos.Player.PlayerDto>
                {
                    new Common.Dtos.Player.PlayerDto
                    {
                        Id = 1
                    },
                }
            };
            var ex = Assert.Throws<UserDataException>(() => _matchModule.EditCreatingMatch(matchDto, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Match_PlayerNotInTournament);
        }

        [Test]
        public void EditCreatingMatch_RemovePlayers()
        {
            var matchDto = new Common.Dtos.Match.MatchCreatingEditDto
            {
                HomeTeamName = "Team A",
                AwayTeamName = "Team B",
                Id = 2,
            };
            _matchModule.EditCreatingMatch(matchDto, 1);
            var match = _soccerDbContext.Matches
                .Include(m => m.HomeTeam.Players).Include(m => m.AwayTeam.Players)
                .FirstOrDefault(m => m.Id == 2);
            Assert.AreEqual(0, match.AwayTeam.Players.Count);
        }

        [Test]
        public void EditGoals_AddGoals()
        {
            var goals = _soccerDbContext.Goals.Count(p => p.Match.Id == 1);
            _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 1,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
            {
                new Common.Dtos.Match.GoalDto
                {
                    ScorerId = 1,
                    AssistId = 2,
                    Time = 5,
                    IsHomeTeam = true,
                },
                new Common.Dtos.Match.GoalDto
                {
                    ScorerId = 2,
                    AssistId = 1,
                    Time = 8,
                    IsHomeTeam = true,
                }
            }
            }, 1);
            Assert.AreEqual(goals + 2, _soccerDbContext.Goals.Count(p => p.Match.Id == 1));
        }

        [Test]
        public void EditGoals_AddGoalNoAssist()
        {
            var goals = _soccerDbContext.Goals.Count(p => p.Match.Id == 1);
            _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 1,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
                {
                    new Common.Dtos.Match.GoalDto
                    {
                        ScorerId = 2,
                        Time = 8,
                    }
                }
            }, 1);
            Assert.AreEqual(goals + 1, _soccerDbContext.Goals.Count(p => p.Match.Id == 1));
        }

        [Test]
        public void EditGoals_AddInvalidScorer()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 1,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
                {
                    new Common.Dtos.Match.GoalDto
                    {
                        Time = 8
                    }
                }
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_InvalidScorer);
        }

        [Test]
        public void EditGoals_AddInvalidAssist()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 1,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
                {
                    new Common.Dtos.Match.GoalDto
                    {
                        ScorerId = 1,
                        AssistId = 12,
                        Time = 8
                    }
                }
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Tournament_InvalidAssist);
        }

        [Test]
        public void EditGoals_RemoveAllGoals()
        {
            _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 2,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
                {
                }
            }, 1);
            Assert.AreEqual(0, _soccerDbContext.Goals.Where(g => g.IsActive).Count(p => p.Match.Id == 2));
        }

        [Test]
        public void AddGoal_Success()
        {
            var goals = _soccerDbContext.Goals.Count(p => p.Match.Id == 2);
            _matchModule.AddGoal(new Common.Dtos.Match.MatchOngoingEditDto
            {
                Id = 2,
                Goal = new Common.Dtos.Match.GoalDto
                {
                    AssistId = 3,
                    ScorerId = 4,
                    Time = 3                   
                }
            }, 1);
            Assert.AreEqual(goals + 1, _soccerDbContext.Goals.Count(p => p.Match.Id == 2));
        }

        [Test]
        public void RemoveGoal_Success()
        {
            var goals = _soccerDbContext.Goals.Count(p => p.Match.Id == 2);
            _matchModule.RemoveGoal(new Common.Dtos.Match.MatchOngoingEditDto
            {
                Id = 2,
                Goal = new Common.Dtos.Match.GoalDto
                {
                    Id = 1
                }
            }, 1);
            Assert.AreEqual(goals - 1, _soccerDbContext.Goals.Count(p => p.Match.Id == 2));
        }

        [Test]
        public void AddGoal_IncorrectState()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.AddGoal(new Common.Dtos.Match.MatchOngoingEditDto
            {
                Id = 1,
                Goal = new Common.Dtos.Match.GoalDto
                {
                    AssistId = 3,
                    ScorerId = 4,
                    Time = 3
                }
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Match_IncorrectState);
        }

        [Test]
        public void RemoveGoal_IncorrectState()
        {
            var ex = Assert.Throws<UserDataException>(() => _matchModule.RemoveGoal(new Common.Dtos.Match.MatchOngoingEditDto
            {
                Id = 1,
                Goal = new Common.Dtos.Match.GoalDto
                {
                    Id = 1
                }
            }, 1));
            Assert.IsTrue(ex.Message == CoreTranslations.Match_IncorrectState);
        }

        [Test]
        public void EditGoals_UpdateGoals()
        {
            var goals = _soccerDbContext.Goals.Count(p => p.Match.Id == 2);
            var goalsHome = _soccerDbContext.Goals.Where(g => g.IsHomeTeam).Count(p => p.Match.Id == 2);
            _matchModule.EditGoals(new Common.Dtos.Match.MatchPendingEditDto
            {
                Id = 2,
                Goals = new System.Collections.Generic.List<Common.Dtos.Match.GoalDto>
                {
                        new Common.Dtos.Match.GoalDto
                        {
                            Id = 1,
                            Time = 11,
                            ScorerId = 3,
                            AssistId = 4,
                            IsHomeTeam = true
                        },
                         new Common.Dtos.Match.GoalDto
                        {
                            Id = 2,
                            Time = 13,
                            ScorerId = 4,
                            AssistId = 3,
                            IsHomeTeam = true
                        }
                }
            }, 1);
            Assert.AreEqual(goals, _soccerDbContext.Goals.Count(p => p.Match.Id == 2));
            Assert.AreEqual(goalsHome + 2, _soccerDbContext.Goals.Where(g => g.IsHomeTeam).Count(p => p.Match.Id == 2));
        }
    }
}