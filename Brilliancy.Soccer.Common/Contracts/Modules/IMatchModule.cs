using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using PagedList;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IMatchModule
    {
        int AddTournamentMatch(NewMatchDto dto, int userId);

        void EditGoals(List<GoalDto> dto, int matchId, int userId);

        MatchEditDto GetMatch(int id, int userId);

        void ChangeMatchStateToPending(int matchId, int userId);

        void ChangeMatchStateToOngoing(int matchId, int userId);

        void ChangeMatchStateToFinished(int matchId, int userId);

        int EditCreatingMatch(MatchCreatingEditDto dto, int userId);
    }
}
