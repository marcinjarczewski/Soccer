using Brilliancy.Soccer.Common.Contracts.Services.MatchObserver;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Services.MatchObserver
{
    public class MatchPublisher : IMatchPublisher
    {
        private static readonly object _lock = new object();

        private List<IMatchSubscriber> _subscribers;
        private List<IMatchSubscriber> _subscribersToRemove;
        private MatchPublisher() { }
        private static MatchPublisher _instance;

        public static MatchPublisher GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MatchPublisher();
                        _instance._subscribers = new List<IMatchSubscriber>();
                        _instance._subscribersToRemove = new List<IMatchSubscriber>();
                    }
                }
            }
            return _instance;
        }

        public void NotifySubscribers(int matchId)
        {
            lock (_lock)
            {
                try
                {
                    foreach (var subscriber in _instance._subscribersToRemove)
                    {
                        _instance._subscribers.Remove(subscriber);
                    }
                }
                catch
                {

                }
                _instance._subscribersToRemove.Clear();
                _instance._subscribers = _instance._subscribers.Where(s => s != null).ToList();
                foreach (var subscriber in _instance._subscribers)
                {
                    subscriber.Update(matchId);
                }
            }
        }

        public void Subscribe(IMatchSubscriber subscriber)
        {
            if (subscriber.HasPrivileges())
            {
                if (!subscriber.UpdatedNow())
                {
                    _instance._subscribers.Add(subscriber);
                }
            }
        }

        public void Unsubscribe(IMatchSubscriber subscriber)
        {
            _instance._subscribersToRemove.Add(subscriber);
        }
    }
}
