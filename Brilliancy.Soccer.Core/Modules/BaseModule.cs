using AutoMapper;
using Brilliancy.Soccer.Common.Exceptions;
using Brilliancy.Soccer.Core.Translations;
using Brilliancy.Soccer.DbModels;
using System;
using System.Linq;

namespace Brilliancy.Soccer.Core.Modules
{
    public class BaseModule 
    {
        protected IMapper _mapper {get;}
        public BaseModule(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected void CheckPrivilages(TournamentDbModel tournament, int userId)
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
