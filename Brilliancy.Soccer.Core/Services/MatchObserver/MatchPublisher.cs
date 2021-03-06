using Brilliancy.Soccer.Common.Contracts.Services.MatchObserver;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brilliancy.Soccer.Core.Services.MatchObserver
{
    public class MatchPublisher : IMatchPublisher
    {
        private static readonly object _lock = new object();

        private List<IMatchSubscriber> _subscribers;
        private List<IMatchSubscriber> _subscribersToRemove;
        private ILogger _logger;
        private MatchPublisher() { }
        private static MatchPublisher _instance;

        public static MatchPublisher GetInstance(ILogger logger)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MatchPublisher();
                        _instance._subscribers = new List<IMatchSubscriber>();
                        _instance._logger = logger;
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
                    _logger.LogInformation($"NotifySubscribers - start");
                    foreach (var subscriber in _instance._subscribersToRemove)
                    {
                        _instance._subscribers.Remove(subscriber);
                    }
                }
                catch(Exception ex)
                {                 
                    _logger.LogInformation($"NotifySubscribersError:" + ex.ToString());
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
