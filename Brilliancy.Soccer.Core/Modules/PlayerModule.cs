using AutoMapper;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Dtos.Authentication;
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
