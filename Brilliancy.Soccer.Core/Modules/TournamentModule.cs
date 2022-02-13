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
    public class TournamentModule : BaseModule, ITournamentModule
    {
        private SoccerDbContext _dbContext { get; }
        public TournamentModule(IMapper mapper, SoccerDbContext context) : base(mapper)
        {
            _dbContext = context;
        }

        public int AddTournament(NewTournamentDto dto)
        {
            if(dto == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            var tournament = _mapper.Map<TournamentDbModel>(dto);
            tournament.IsActive = true;
            tournament.Owner = _dbContext.Users.FirstOrDefault(u => u.Id == tournament.OwnerId);
            if(tournament.Owner == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoUser);
            }
            this._dbContext.Tournaments.Add(tournament);
            this._dbContext.SaveChanges();
            return tournament.Id;
        }

        public void EditTournament(NewTournamentDto dto, int userId)
        {
            if (dto == null || !dto.Id.HasValue)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            var tournament = _dbContext.Tournaments.FirstOrDefault(t => t.Id == dto.Id);
            if(tournament == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            CheckPrivilages(tournament, userId);
            tournament.Address = dto.Address;
            tournament.DefaultDayOfTheWeek = dto.DefaultDayOfTheWeek;
            tournament.DefaultHour = dto.DefaultHour;
            tournament.Name = dto.Name;
            this._dbContext.Tournaments.Update(tournament);
            this._dbContext.SaveChanges();
        }

        public void DeleteTournament(int id, int userId)
        {
            var tournament = _dbContext.Tournaments.Include(t => t.Owner).Include(t => t.Admins).FirstOrDefault(t => t.Id == id);
            CheckPrivilages(tournament, userId);
            tournament.IsActive = false;
            this._dbContext.SaveChanges();
        }

        public void ActivateTournament(int id, int userId)
        {
            var tournament = _dbContext.Tournaments.Include(t => t.Owner).Include(t => t.Admins).FirstOrDefault(t => t.Id == id);
            CheckPrivilages(tournament, userId);
            tournament.IsActive = true;
            this._dbContext.SaveChanges();
        }

        public TournamentDto GetTournament(int id, int userId)
        {
            var tournament = _dbContext.Tournaments.Include(t => t.Owner).Include(t => t.Admins).FirstOrDefault(t => t.Id == id);
            if (tournament == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            if (!tournament.IsActive)
            {
                if (tournament.OwnerId != userId && tournament.Admins?.FirstOrDefault(a => a.Id == userId) == null)
                {
                    throw new UserDataException(CoreTranslations.Tournament_NoTournament);
                }
            }

            return _mapper.Map<TournamentDto>(tournament);
        }

        public IPagedList<TournamentDto> GetTournaments(string term, int pageNumber, int pageSize)
        {
            var tournaments = _dbContext.Tournaments.AsQueryable();
            if(!string.IsNullOrEmpty(term))
            {
                var lowerTerm = term.ToLower();
                tournaments = tournaments.Where(t => t.Name.ToLower().Contains(lowerTerm) || t.Address.ToLower().Contains(lowerTerm));
            }
            var page =
            tournaments.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (page == null)
            {
                return new StaticPagedList<TournamentDto>(new List<TournamentDto>(), pageNumber, pageSize, 0);
            }

            var total = tournaments.Count();
            var tournamentDtos =_mapper.Map<List<TournamentDto>>(page);

            return new StaticPagedList<TournamentDto>(tournamentDtos, pageNumber, pageSize, total);
        }

        private void CheckPrivilages(TournamentDbModel tournament, int userId)
        {
            if (tournament == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoTournament);
            }
            if (tournament.OwnerId != userId && tournament.Admins?.FirstOrDefault(a => a.Id == userId) == null)
            {
                throw new UserDataException(CoreTranslations.Tournament_NoPrivileges);
            }
        }
    }
}
