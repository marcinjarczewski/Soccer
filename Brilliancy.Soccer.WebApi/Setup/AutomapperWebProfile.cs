
using AutoMapper;
using Brilliancy.Soccer.Common.Contracts;
using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.Login;
using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Player;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using Brilliancy.Soccer.WebApi.Models.Authentication.Write;
using Brilliancy.Soccer.WebApi.Models.Login.Write;
using Brilliancy.Soccer.WebApi.Models.Match.Read;
using Brilliancy.Soccer.WebApi.Models.Match.Write;
using Brilliancy.Soccer.WebApi.Models.Player.Read;
using Brilliancy.Soccer.WebApi.Models.Player.Write;
using Brilliancy.Soccer.WebApi.Models.Read.Tournament;
using Brilliancy.Soccer.WebApi.Models.User.Read;
using Brilliancy.Soccer.WebApi.Models.Write.Tournament;
using System.Collections.Generic;
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
            CreateMap<GoalDto, GoalReadModel>();
            CreateMap<GoalWriteModel, GoalDto>();
            CreateMap<SingleGoalWriteModel, GoalDto>();
            CreateMap<MatchEditDto, MatchDetailsModel>()
                .ForMember(dto => dto.HomeGoalsList, m => m.MapFrom(db => db.Goals.Where(g => g.IsHomeTeam)))
                .ForMember(dto => dto.AwayGoalsList, m => m.MapFrom(db => db.Goals.Where(g => !g.IsHomeTeam)));
            CreateMap<PendingMatchWriteModel, MatchPendingEditDto>()
                .ForMember(dto => dto.Goals, m => m.MapFrom<GoalFormatter>());
            CreateMap<PendingMatchWriteModel, MatchPendingEditDto>()
           .ForMember(dto => dto.Goals, m => m.MapFrom<GoalFormatter>());
            CreateMap<MatchOngoingEditModel, MatchOngoingEditDto>(); 
            CreateMap<MatchEditDto, MatchReadModel>();
            CreateMap<TournamentDto, EditTournamentReadModel>();
            CreateMap<PlayerDto, PlayerReadModel>();
            CreateMap<AdminDto, AdminReadModel>();
            CreateMap<PlayerWriteModel, PlayerDto>()
                .ForMember(dto => dto.Id, m => m.MapFrom(model => model.Id == 0 ? default(int?) : model.Id));
            CreateMap<CreatingMatchWriteModel, MatchCreatingEditDto>();
            CreateMap<NewMatchWriteModel, NewMatchDto>();
            CreateMap<EditTournamentWriteModel, TournamentDto>();
            CreateMap<AuthenticationWriteModel, AuthenticationDto>(); 
            CreateMap<LoginDto, UserInfo>()
               .ForMember(dto => dto.IsAdmin, m => m.MapFrom(db => (db.Roles ?? new System.Collections.Generic.List<RoleDto>()).Any(r => r.Id == (int)RoleEnum.Admin)));
        }
    }

    public class GoalFormatter : IValueResolver<PendingMatchWriteModel, MatchPendingEditDto, List<GoalDto>>
    {
        public List<GoalDto> Convert(PendingMatchWriteModel source, MatchPendingEditDto dto, ResolutionContext context)
        {
            var goals = source.HomeGoalsList.Select(s => MapGoals(s, true)).ToList();
            goals.AddRange(source.AwayGoalsList.Select(s => MapGoals(s, false)));
            return goals;
        }

        public GoalDto MapGoals(GoalWriteModel model, bool isHomeTeam)
        {          
            return new GoalDto
            {
                AssistId = model.AssistId,
                Id = model.Id,
                IsOwnGoal = model.IsOwnGoal,
                ScorerId = model.ScorerId,
                Time = model.Time,
                IsHomeTeam = isHomeTeam
            };
        }

        public List<GoalDto> Resolve(PendingMatchWriteModel source, MatchPendingEditDto destination, List<GoalDto> destMember, ResolutionContext context)
        {
            var goals = source.HomeGoalsList?.Select(s => MapGoals(s, true)).ToList() ?? new List<GoalDto>();
            if (source.AwayGoalsList != null && source.AwayGoalsList.Any())
            {
                goals.AddRange(source.AwayGoalsList.Select(s => MapGoals(s, false)));
            }
            destMember = goals;
            return goals;
        }
    }
}
