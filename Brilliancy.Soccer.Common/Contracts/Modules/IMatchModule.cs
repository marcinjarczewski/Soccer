using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Dtos.Tournament;
using Brilliancy.Soccer.Common.Dtos.User;
using PagedList;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IMatchModule
    {
        int AddTournamentMatch(NewMatchDto dto, int userId);

        void EditGoals(MatchPendingEditDto dto, int userId);

        MatchEditDto GetMatch(int id, int userId, bool withReload = false);

        bool CanUserGetMatch(int id, int userId);

        void ChangeMatchStateToPending(int matchId, int userId);

        void ChangeMatchStateToOngoing(int matchId, int userId);

        void ChangeMatchStateToCanceled(int matchId, int userId);

        void ChangeMatchStateToFinished(int matchId, int userId);

        void ChangeMatchStateToCreating(int matchId, int userId);

        int EditCreatingMatch(MatchCreatingEditDto dto, int userId);

        void AddGoal(MatchOngoingEditDto dto, int userId);

        void RemoveGoal(MatchOngoingEditDto dto, int userId);

        DateTime LastMatchUpdate(int id);
    }
}
