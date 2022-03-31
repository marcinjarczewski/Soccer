using Brilliancy.Soccer.Common.Dtos.Match;
using Brilliancy.Soccer.Common.Enums;

namespace Brilliancy.Soccer.Common.Contracts.Services.MatchObserver
{
    public interface IMatchPublisher
    {
        void Subscribe(IMatchSubscriber subscriber);

        void Unsubscribe(IMatchSubscriber subscriber);

        void NotifySubscribers(int matchId);
    }
}
