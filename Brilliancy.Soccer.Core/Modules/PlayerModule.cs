using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
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
    public class PlayerModule : BaseModule, IPlayerModule
    {
        private SoccerDbContext _dbContext { get; }
        public PlayerModule(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }

        public int AddTournamentPlayer(NewPlayerDto dto, int tournamentId, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoPlayer);
            }
            if (string.IsNullOrEmpty(dto.FirstName) && string.IsNullOrEmpty(dto.LastName) && string.IsNullOrEmpty(dto.NickName))
            {
                throw new UserDataException(CoreTranslations.Tournament_NoPlayer);
            }
            var tournament = _dbContext.Tournaments.Include(t => t.Players).FirstOrDefault(t => t.Id == tournamentId);
            if (tournament == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            CheckPrivilages(tournament, userId);
            var player = _mapper.Map<PlayerDbModel>(dto);
            tournament.Players.Add(player);
            this._dbContext.Tournaments.Update(tournament);
            this._dbContext.SaveChanges();
            return player.Id;
        }

        public void EditPlayers(List<PlayerDto> dto, int tournamentId, int userId)
        {
            if (dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoGoals);
            }

            var tournament = _dbContext.Tournaments
                .Include(t => t.Players)
                .Include(t => t.Admins)
                .FirstOrDefault(t => t.Id == tournamentId);
            if (tournament == null || !tournament.IsActive)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            CheckPrivilages(tournament, userId);

            var playerIdList = dto.Where(g => g.Id.HasValue).Select(g => g.Id).ToList();
            //remove players
            var removedPlayers = tournament.Players.Where(g => !playerIdList.Contains(g.Id)).ToList();
            foreach (var removedPlayer in removedPlayers)
            {
                removedPlayer.IsActive = false;
            }
            //update players
            foreach (var player in tournament.Players)
            {
                if (player.IsActive)
                {
                  
                    var playerDto = dto.FirstOrDefault(g => g.Id == player.Id);
                    if (playerDto != null)
                    {
                        if (string.IsNullOrEmpty(player.FirstName) && string.IsNullOrEmpty(player.LastName) && string.IsNullOrEmpty(player.NickName))
                        {
                            throw new UserDataException(CoreTranslations.Tournament_NoPlayer);
                        }
                        player.FirstName = playerDto.FirstName;
                        player.LastName = playerDto.LastName;
                        player.NickName = playerDto.NickName;
                    }
                }
            }
            //add players
            foreach (var playerDto in dto.Where(g => !g.Id.HasValue))
            {
                if (string.IsNullOrEmpty(playerDto.FirstName) && string.IsNullOrEmpty(playerDto.LastName) && string.IsNullOrEmpty(playerDto.NickName))
                {
                    throw new UserDataException(CoreTranslations.Tournament_NoPlayer);
                }
                tournament.Players.Add(new PlayerDbModel
                {
                    FirstName = playerDto.FirstName,
                    IsActive = true,
                    LastName = playerDto.LastName,
                    NickName = playerDto.NickName,                 
                });
            }

            this._dbContext.Tournaments.Update(tournament);
            this._dbContext.SaveChanges();
        }

        public void RemovePlayerFromTournament(int playerId, int userId)
        {
            var player = _dbContext.Players.Include(p => p.Tournament.Admins).Include(p => p.User).FirstOrDefault(p => p.Id == playerId);
            if(player == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoPlayer);
            }
            if (player.UserId != userId)
            {
                CheckPrivilages(player.Tournament, userId);
            }
            player.IsActive = false;
            this._dbContext.SaveChanges();
        }
    }
}
