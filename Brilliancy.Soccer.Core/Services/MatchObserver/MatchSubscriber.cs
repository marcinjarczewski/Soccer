using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Common.Contracts.Services.MatchObserver;
using Brilliancy.Soccer.Common.Dtos.Match;
using System;
using System.Threading;

namespace Brilliancy.Soccer.Core.Services.MatchObserver
{
    public class MatchSubscriber : IMatchSubscriber
    {
        private int _matchId { get; }

        private int _userId { get; }

        private DateTime _matchUpdateDate { get; }

        private IMatchModule _matchModule { get; }

        private bool _shouldBeUpdated;

        public MatchSubscriber(int matchId, int userId, DateTime matchUpdateDate, IMatchModule matchModule)
        {
            _matchId = matchId;
            _userId = userId;
            _matchUpdateDate = matchUpdateDate;
            _matchModule = matchModule;
        }

        private AutoResetEvent _waitHandle = new AutoResetEvent(false);

        public void Update(int matchId)
        {
            if (matchId == _matchId)
            {
                    _shouldBeUpdated = true;
                    _waitHandle.Set();
            }
        }

        public Tuple<bool, MatchEditDto> WaitForResult(int sleepTimeInSeconds)
        {            
            var publisher = MatchPublisher.GetInstance();
            publisher.Subscribe(this);
            _waitHandle.WaitOne(sleepTimeInSeconds * 1000);
            publisher.Unsubscribe(this);
            if (_shouldBeUpdated)
            {
                if (_shouldBeUpdated)
                {
                    return new Tuple<bool, MatchEditDto>(true, _matchModule.GetMatch(_matchId, _userId, true));
                }
            }

            return new Tuple<bool, MatchEditDto>(false, null);
        }

        public bool HasPrivileges()
        {
            return _matchModule.CanUserGetMatch(_matchId, _userId);
        }

        public bool UpdatedNow()
        {
            var date = _matchModule.LastMatchUpdate(_matchId);
            if (date > _matchUpdateDate)
            {
                Update(_matchId);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
