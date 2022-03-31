using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Common.Helpers;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbAccess;
using Brilliancy.Soccer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class MatchModule : BaseModule, IMatchModule
    {
        private SoccerDbContext _dbContext { get; }

        private ILogger<MatchModule> _logger { get; }
        public MatchModule(IMapper mapper, ILogger<MatchModule> logger, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
            _logger = logger;
        }

        public DateTime LastMatchUpdate(int id)
        {
            var match = _dbContext.Matches.FirstOrDefault(t => t.Id == id);
            return match?.LastUpdateDate ?? DateTime.Now;
        }

        public bool CanUserGetMatch(int id, int userId)
        {
            try
            {
                var dbModel = _dbContext.Matches
                 .Include(t => t.Tournament.Players)
                 .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == id);
                ValidateMatchAccess(dbModel, userId);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        private void ValidateMatchAccess(MatchDbModel dbModel, int userId)
        {
            if (dbModel.Tournament.OwnerId != userId && !dbModel.Tournament.Players.Any(p => p.UserId == userId) && !dbModel.Tournament.Admins.Any(p => p.Id == userId))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
        }

        public MatchEditDto GetMatch(int id, int userId, bool withReload = false)
        {
            var match = _dbContext.Matches
             .Include(t => t.HomeTeam.Players)
             .Include(t => t.AwayTeam.Players)
             .Include(t => t.Goals).ThenInclude(g => g.Assist)
             .Include(t => t.Goals).ThenInclude(g => g.Scorer)
             .Include(t => t.State)
             .Include(t => t.Tournament.Players)
             .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == id);
            if(withReload)
            {
                _dbContext.Entry(match).Reload();
            }
            var result = _mapper.Map<MatchEditDto>(match);
            if (match == null)
            {
                throw new InvalidDataException(CoreTranslations.Tournament_NoMatch);
            }
            result.Goals = _mapper.Map<List<GoalDto>>(match.Goals.Where(g => g.IsActive));
            var homeIdPlayers = result.HomePlayers.Select(p => p.Id).ToList();
            var awayIdPlayers = result.AwayPlayers.Select(p => p.Id).ToList();
            var availablePlayers = match.Tournament.Players.Where(p => p.IsActive && !homeIdPlayers.Contains(p.Id)).Where(p => !awayIdPlayers.Contains(p.Id)).ToList();
            result.AvailablePlayers = _mapper.Map<List<PlayerDto>>(availablePlayers);
            return result;
        }

        private void CheckTeamNames(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoHomeTeam);
            }
            if (string.IsNullOrEmpty(awayTeam))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoAwayTeam);
            }
            if (homeTeam == awayTeam)
            {
                throw new UserDataException(CoreTranslations.Tournament_SameTeams);
            }
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
            CheckTeamNames(dto.HomeTeamName, dto.AwayTeamName);
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
            match.Date = NextMatchHelper.GetMatchDate(DateTime.Now, tournament.DefaultDayOfTheWeek, tournament.DefaultHour);
            match.LastUpdateDate = DateTime.Now;
            match.IsActive = true;
            match.State = state;
            tournament.Matches.Add(match);
            this._dbContext.Tournaments.Update(tournament);
            this._dbContext.SaveChanges();
            return match.Id;
        }

        public void ChangeMatchStateToPending(int matchId, int userId)
        {
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            if (!new List<int> { (int)MatchStateEnum.Creating, (int)MatchStateEnum.Finished }.Contains(match.StateId))
            {
                throw new UserDataException(CoreTranslations.Match_IncorrectState);
            }
            match.StateId = (int)MatchStateEnum.Pending;
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public void ChangeMatchStateToFinished(int matchId, int userId)
        {
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            if (!new List<int> { (int)MatchStateEnum.Ongoing, (int)MatchStateEnum.Pending }.Contains(match.StateId))
            {
                throw new UserDataException(CoreTranslations.Match_IncorrectState);
            }
            match.StateId = (int)MatchStateEnum.Finished;
            match.EndDate = DateTime.Now;
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public void ChangeMatchStateToOngoing(int matchId, int userId)
        {
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            if (!new List<int> { (int)MatchStateEnum.Pending }.Contains(match.StateId))
            {
                throw new UserDataException(CoreTranslations.Match_IncorrectState);
            }
            match.StateId = (int)MatchStateEnum.Ongoing;
            match.StartDate = DateTime.Now;
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public void ChangeMatchStateToCreating(int matchId, int userId)
        {
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            match.StateId = (int)MatchStateEnum.Creating;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public void ChangeMatchStateToCanceled(int matchId, int userId)
        {
            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == matchId);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            match.StateId = (int)MatchStateEnum.Canceled;
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
        }

        public int EditCreatingMatch(MatchCreatingEditDto dto, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }

            var match = _dbContext.Matches
                .Include(t => t.HomeTeam.Players)
                .Include(t => t.AwayTeam.Players)
                .Include(t => t.Tournament.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == dto.Id);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            CheckTeamNames(dto.HomeTeamName, dto.AwayTeamName);
            match.HomeTeam.Name = dto.HomeTeamName;
            match.AwayTeam.Name = dto.AwayTeamName;
            match.Date = dto.Date;
            match.LastUpdateDate = DateTime.Now;

            var homePlayersDto = dto.HomePlayers ?? new List<PlayerDto>();
            var homePlayersId = homePlayersDto.Where(g => g.Id.HasValue).Select(g => g.Id).ToList();
            //remove home players
            var removedHomePlayers = match.HomeTeam.Players.Where(g => !homePlayersId.Contains(g.Id)).ToList();
            foreach (var removedPlayer in removedHomePlayers)
            {
                match.HomeTeam.Players.Remove(removedPlayer);
            }

            //add home players
            foreach (var homePlayerDto in homePlayersDto)
            {
                if (!match.HomeTeam.Players.Any(p => p.Id == homePlayerDto.Id))
                {
                    var player = match.Tournament.Players.FirstOrDefault(p => p.Id == homePlayerDto.Id);
                    if (player == null)
                    {
                        throw new UserDataException(CoreTranslations.Match_PlayerNotInTournament);
                    }
                    match.HomeTeam.Players.Add(player);
                }
            }

            var awayPlayersDto = dto.AwayPlayers ?? new List<PlayerDto>();
            var awayPlayersId = awayPlayersDto.Where(g => g.Id.HasValue).Select(g => g.Id).ToList();
            //remove away players
            var removedAwayPlayers = match.AwayTeam.Players.Where(g => !awayPlayersId.Contains(g.Id)).ToList();
            foreach (var removedPlayer in removedAwayPlayers)
            {
                match.AwayTeam.Players.Remove(removedPlayer);
            }

            //add away players
            foreach (var awayPlayerDto in awayPlayersDto)
            {
                if (!match.AwayTeam.Players.Any(p => p.Id == awayPlayerDto.Id))
                {
                    var player = match.Tournament.Players.FirstOrDefault(p => p.Id == awayPlayerDto.Id);
                    match.AwayTeam.Players.Add(player);
                }
            }

            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
            return match.Id;
        }

        public void EditGoals(MatchPendingEditDto dto, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }

            var match = _dbContext.Matches
                .Include(t => t.Goals)
                .Include(t => t.Tournament.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == dto.Id);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            CheckPrivilages(match.Tournament, userId);
            match.Date = dto.Date;
            match.HomeGoals = dto.HomeGoals;
            match.AwayGoals = dto.AwayGoals;
            match.LastUpdateDate = DateTime.Now;

            var goalIdList = dto.Goals.Where(g => g.Id.HasValue).Select(g => g.Id).ToList();
            //remove goals
            var removedGoals = match.Goals.Where(g => !goalIdList.Contains(g.Id)).ToList();
            foreach (var removedGoal in removedGoals)
            {
                removedGoal.IsActive = false;
            }
            //update goals
            foreach (var goal in match.Goals)
            {
                if (goal.IsActive)
                {
                    var goalDto = dto.Goals.FirstOrDefault(g => g.Id == goal.Id);
                    if (goalDto != null)
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
            foreach (var goal in dto.Goals.Where(g => !g.Id.HasValue || g.Id == 0))
            {
                var scorer = match.Tournament.Players.FirstOrDefault(p => p.Id == goal.ScorerId);
                if (scorer == null)
                {
                    throw new UserDataException(CoreTranslations.Tournament_InvalidScorer);
                }
                PlayerDbModel assist = null;
                if (goal.AssistId.HasValue)
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
            _logger.LogInformation($"Match {match.Id} int tournament {match.TournamentId} edited by {userId}");
        }

        public void DeleteMatchFromTournament(int matchId, int userId)
        {
            var match = _dbContext.Matches.Include(p => p.Tournament.Admins).FirstOrDefault(p => p.Id == matchId);
            if (match == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }

            CheckPrivilages(match.Tournament, userId);
            match.IsActive = false;
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.SaveChanges();
            _logger.LogInformation($"Match {match.Id} int tournament {match.TournamentId} deleted by {userId}");
        }

        public void AddGoal(MatchOngoingEditDto dto, int userId)
        {
            if (dto == null || dto.Goal == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }

            var match = _dbContext.Matches
                .Include(t => t.Goals)
                .Include(t => t.Tournament.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == dto.Id);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            if (match.StateId != (int)MatchStateEnum.Ongoing)
            {
                throw new UserDataException(CoreTranslations.Match_IncorrectState);
            }
            CheckPrivilages(match.Tournament, userId);

            {
                var scorer = match.Tournament.Players.FirstOrDefault(p => p.Id == dto.Goal.ScorerId);
                if (scorer == null)
                {
                    throw new UserDataException(CoreTranslations.Tournament_InvalidScorer);
                }
                PlayerDbModel assist = null;
                if (dto.Goal.AssistId.HasValue)
                {
                    assist = match.Tournament.Players.FirstOrDefault(p => p.Id == dto.Goal.AssistId);
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
                    IsOwnGoal = dto.Goal.IsOwnGoal,
                    Time = dto.Goal.Time,
                    IsHomeTeam = dto.Goal.IsHomeTeam
                });
            }
            match.HomeGoals = match.Goals.Count(m => m.IsActive && ((m.IsHomeTeam && !m.IsOwnGoal) || (!m.IsHomeTeam && m.IsOwnGoal)));
            match.AwayGoals = match.Goals.Count(m => m.IsActive && ((m.IsHomeTeam && m.IsOwnGoal) || (!m.IsHomeTeam && !m.IsOwnGoal)));
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
            _logger.LogInformation($"Goal in match {match.Id} added by {userId}");
        }

        public void RemoveGoal(MatchOngoingEditDto dto, int userId)
        {
            if (dto == null || dto.Goal == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }

            var match = _dbContext.Matches
                .Include(t => t.Goals)
                .Include(t => t.Tournament.Players)
                .Include(t => t.Tournament.Admins).FirstOrDefault(t => t.Id == dto.Id);
            if (match == null || !match.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoMatch);
            }
            if (match.StateId != (int)MatchStateEnum.Ongoing)
            {
                throw new UserDataException(CoreTranslations.Match_IncorrectState);
            }
            CheckPrivilages(match.Tournament, userId);

            var goal = match.Goals.FirstOrDefault(g => g.Id == dto.Goal.Id);
            if (goal == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }
            match.Goals.Remove(goal);
            match.HomeGoals = match.Goals.Count(m => m.IsActive && ((m.IsHomeTeam && !m.IsOwnGoal) || (!m.IsHomeTeam && m.IsOwnGoal)));
            match.AwayGoals = match.Goals.Count(m => m.IsActive && ((m.IsHomeTeam && m.IsOwnGoal) || (!m.IsHomeTeam && !m.IsOwnGoal)));
            match.LastUpdateDate = DateTime.Now;
            this._dbContext.Matches.Update(match);
            this._dbContext.SaveChanges();
            _logger.LogInformation($"Goal in match {match.Id} removed by {userId}");
        }
    }
}
