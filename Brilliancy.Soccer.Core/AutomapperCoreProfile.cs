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
            CreateMap<GoalDbModel, GoalDto>()
                .ForMember(dto => dto.ScorerPlayerName, m => m.MapFrom(db => db.Scorer.FirstName + " " + db.Scorer.NickName + " " + db.Scorer.LastName))
                .ForMember(dto => dto.AssistPlayerName, m => m.MapFrom(db => db.Assist != null ? (db.Assist.FirstName + " " + db.Assist.NickName + " " + db.Assist.LastName) : ""));
            CreateMap<PlayerDbModel, PlayerDto>();
            CreateMap<MatchDbModel, MatchEditDto>()
                .ForMember(dto => dto.HomePlayers, m => m.MapFrom(db => db.HomeTeam.Players))
                .ForMember(dto => dto.AwayPlayers, m => m.MapFrom(db => db.AwayTeam.Players))
                .ForMember(dto => dto.StateName, m => m.MapFrom(db => ((MatchStateEnum)db.StateId).ToTranslatedString()));
            CreateMap<NewTournamentDto, TournamentDbModel>();
            CreateMap<NewPlayerDto, PlayerDbModel>();
            CreateMap<NewMatchDto, MatchDbModel>();
            CreateMap<TournamentDbModel, TournamentDto>();
        }
    }

}

