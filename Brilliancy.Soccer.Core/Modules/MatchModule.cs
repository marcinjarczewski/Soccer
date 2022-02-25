using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class MatchModule : BaseModule, IMatchModule
    {
        private SoccerDbContext _dbContext { get; }
        public MatchModule(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }

        public MatchEditDto GetMatch(int id, int userId)
        {
            var match = _dbContext.Matches
             .Include(t => t.HomeTeam.Players)
             .Include(t => t.AwayTeam.Players)
             .Include(t => t.State)
             .Include(t => t.Tournament.Players)
             .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == id);
            var result = _mapper.Map<MatchEditDto>(match);
            var homeIdPlayers = result.HomePlayers.Select(p => p.Id).ToList();
            var awayIdPlayers = result.AwayPlayers.Select(p => p.Id).ToList();
            var availablePlayers = match.Tournament.Players.Where(p => p.IsActive && !homeIdPlayers.Contains(p.Id)).Where(p => !awayIdPlayers.Contains(p.Id)).ToList();
            result.AvailablePlayers = _mapper.Map<List<PlayerDto>>(availablePlayers);
            return result;
        }

        public int AddTournamentMatch(NewMatchDto dto, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            var tournament = _dbContext.Tournaments.Include(t => t.Matches).FirstOrDefault(t => t.Id == dto.TournamentId);
            if (tournament == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            CheckPrivilages(tournament, userId);

            if (string.IsNullOrEmpty(dto.HomeTeamName))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoHomeTeam);
    
            }
            if (string.IsNullOrEmpty(dto.AwayTeamName))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoAwayTeam);
            }
            if (dto.HomeTeamName == dto.AwayTeamName)
            {
                throw new UserDataException(CoreTranslations.Tournament_SameTeams);
            }
            var state = _dbContext.MatchStates.FirstOrDefault(f => f.Id == (int)MatchStateEnum.Creating);
            var match = _mapper.Map<MatchDbModel>(dto);
            match.HomeTeam = new TeamDbModel
            {
                IsActive = true,
                Name = dto.HomeTeamName,
                TournamentId = dto.TournamentId
            };
            match.AwayTeam = new TeamDbModel
            {
                IsActive = true,
                Name = dto.AwayTeamName,
                TournamentId = dto.TournamentId
            };
            match.IsActive = true;
            match.State = state;
            tournament.Matches.Add(match);
            this._dbContext.Tournaments.Update(tournament);
            this._dbContext.SaveChanges();
            return match.Id;
        }

        public int EditTournamentMatch(MatchEditDto dto, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
          
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam)
                .Include(t => t.AwayTeam)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == dto.Id);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            match.HomeTeam.Name = dto.HomeTeamName;
            match.AwayTeam.Name = dto.AwayTeamName;
            match.AwayGoals = dto.AwayGoals;
            match.Date = dto.Date;
            match.HomeGoals = dto.HomeGoals;
            match.HalfAwayGoals = dto.HalfAwayGoals;
            match.HalfHomeGoals = dto.HalfHomeGoals;

            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
            return match.Id;
        }

        public void EditGoals(List<GoalDto> dto, int matchId, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }

            var match = _dbContext.Matches
                .Include(t => t.Goals)
                .Include(t => t.Tournament.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);

            var goalIdList = dto.Where(g => g.Id.HasValue).Select(g => g.Id).ToList();
            //remove goals
            var removedGoals = match.Goals.Where(g => !goalIdList.Contains(g.Id)).ToList();
            foreach (var removedGoal in removedGoals)
            {
                removedGoal.IsActive = false;
            }
            //update goals
            foreach (var goal in match.Goals)
            {
                if(goal.IsActive)
                {
                    var goalDto = dto.FirstOrDefault(g => g.Id == goal.Id);
                    if(goalDto != null)
                    {
                        goal.AssistId = goalDto.AssistId;
                        goal.ScorerId = goalDto.ScorerId;
                        goal.Time = goalDto.Time;
                        goal.IsOwnGoal = goalDto.IsOwnGoal;
                        goal.IsHomeTeam = goalDto.IsHomeTeam;
                    }
                }
            }
            //add goals
            foreach (var goal in dto.Where(g => !g.Id.HasValue))
            {
                var scorer = match.Tournament.Players.FirstOrDefault(p => p.Id == goal.ScorerId);
                if(scorer == null)
                {
                    throw new UserDataException(CoreTranslations.Tournament_InvalidScorer);
                }
                PlayerDbModel assist = null;
                if(goal.AssistId.HasValue)
                {
                    assist = match.Tournament.Players.FirstOrDefault(p => p.Id == goal.AssistId);
                    if (assist == null)
                    {
                        throw new UserDataException(CoreTranslations.Tournament_InvalidAssist);
                    }
                }
                match.Goals.Add(new GoalDbModel
                {
                    Scorer = scorer,
                    Assist = assist,
                    IsActive = true,
                    IsOwnGoal = goal.IsOwnGoal,
                    Time = goal.Time,
                    IsHomeTeam = goal.IsHomeTeam
                }); 
            }

            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public void DeleteMatchFromTournament(int matchId, int userId)
        {
            var match = _dbContext.Matches.Include(p => p.Tournament.Admins).FirstOrDefault(p => p.Id == matchId);
            if(match == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }

            CheckPrivilages(match.Tournament, userId);
            match.IsActive = false;
            this._dbContext.SaveChanges();
        }
    }
}
 