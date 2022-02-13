using AutoMapper;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
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
            CreateMap<NewTournamentDto, TournamentDbModel>();
            CreateMap<TournamentDbModel, TournamentDto>();
        }
    }

}

