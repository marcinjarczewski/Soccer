using Brilliancy.Soccer.Common.Dtos.Match;
using System;

namespace Brilliancy.Soccer.Common.Contracts.Services.MatchObserver
{
    public interface IMatchSubscriber
    {
        void Update(int matchId);

        bool HasPrivileges();

        public bool UpdatedNow();

        Tuple<bool, MatchEditDto> WaitForResult(int sleepTimeInSeconds);
    }
}
