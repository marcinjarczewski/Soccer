using AutoMapper;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Core
{
    public class AutomapperCoreProfile : Profile
    {
        public AutomapperCoreProfile()
        {
            CreateMap<RegisterUserDto, UserDbModel>();
            CreateMap<UserDbModel, LoginDto>();
            CreateMap<RoleDbModel, RoleDto>();
            CreateMap<UserDbModel, UserDto>();
            CreateMap<PlayerDbModel, PlayerDto>();
            CreateMap<MatchDbModel, MatchEditDto>()
                .ForMember(db => db.StateName, m => m.MapFrom(db => ((MatchStateEnum)db.StateId).ToString()));
            CreateMap<NewTournamentDto, TournamentDbModel>();
            CreateMap<NewPlayerDto, PlayerDbModel>();
            CreateMap<NewMatchDto, MatchDbModel>();
            CreateMap<TournamentDbModel, TournamentDto>();
        }
    }

}

