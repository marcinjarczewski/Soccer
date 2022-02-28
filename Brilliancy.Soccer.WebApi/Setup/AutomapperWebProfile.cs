
using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Player.Read;
using Brilliancy.Soccer.WebApi.Models.Player.Write;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.User.Read;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using System.Linq;

namespace Brilliancy.Soccer.WebApi.Setup
{
    public class AutomapperWebProfile : Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<RegisterWriteModel, RegisterUserDto>();
            CreateMap<NewTournamentModel, NewTournamentDto>();
            CreateMap<UserDto, UserInfo>();
            CreateMap<UserDto, UserReadModel>();
            CreateMap<MatchEditDto, MatchDetailsModel>();
            CreateMap<MatchEditDto, MatchReadModel>();
            CreateMap<TournamentDto, EditTournamentModel>();
            CreateMap<PlayerDto, PlayerReadModel>();
            CreateMap<PlayerWriteModel, PlayerDto>()
                .ForMember(dto => dto.Id, m => m.MapFrom(model => model.Id == 0 ? default(int?) : model.Id));
            CreateMap<CreatingMatchWriteModel, MatchCreatingEditDto>();
            CreateMap<NewMatchWriteModel, NewMatchDto>();
            CreateMap<LoginDto, UserInfo>()
               .ForMember(dto => dto.IsAdmin, m => m.MapFrom(db => (db.Roles ?? new System.Collections.Generic.List<RoleDto>()).Any(r => r.Id == (int)RoleEnum.Admin)));
        }
    }
}
