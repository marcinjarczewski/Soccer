using AutoMapper;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Email;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.Login;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliancy.Soccer.Core
{
    public class AutomapperCoreProfile : Profile
    {
        public AutomapperCoreProfile()
        {
            CreateMap<RegisterUserDto, UserDbModel>();
            CreateMap<UserDbModel, LoginDto>()
                .ForMember(dto => dto.Roles, m => m.MapFrom(db => db.UserRoles.Select(u => new RoleDto { Id = u.RoleId, Name = ((RoleEnum)u.RoleId).ToString() }).ToList()));
            CreateMap<RoleDbModel, RoleDto>();
            CreateMap<UserDbModel, UserDto>();
            CreateMap<FileDbModel, FileDto>();
            CreateMap<EmailDbModel, EmailDto>();
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
            CreateMap<TournamentDbModel, TournamentDto>()
                     .ForMember(dto => dto.LogoId, m => m.MapFrom(db => db.Logo != null ? db.Logo.Id : default(int?)))
                     .ForMember(dto => dto.LogoUrl, m => m.MapFrom(db => db.Logo != null ? db.Logo.Url : string.Empty));
        }
    }

}

