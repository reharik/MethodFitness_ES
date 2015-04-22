using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System.Linq;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure.BaseClasses
{
    public interface IDispatcher
    {
        void StartDispatching();
        void StopDispatching();
    }

    public class DispatcherBase : IDispatcher
    {
        private readonly List<IHandler> _eventHandlers;
        private readonly IUIResponsePoster _uiResponsePoster;
        private readonly ILogger _logger;
        protected IEventStoreConnection _gesConnection;
        private bool _stopRequested;
        private EventStoreAllCatchUpSubscription _subscription;
        private BroadcastBlock<IGESEvent> _broadcastBlock;
        protected string _targetTypeName;
        protected Func<ResolvedEvent, bool> _eventFilter;
        private Position position;
        protected bool _isLive;
        private UINotification _responseMessage;

        public DispatcherBase(IGESConnection gesConnection, List<IHandler> eventHandlers, IUIResponsePoster uiResponsePoster, ILogger logger)
        {
            _gesConnection = gesConnection.BuildConnection();
            _gesConnection.ConnectAsync();
            _eventHandlers = eventHandlers;
            _uiResponsePoster = uiResponsePoster;
            _logger = logger;
            // add all handlers to the broadcast block so they receive news of events
            RegisterHandlers();
            // gets last event processed to avoid re processing events after a shut down.
            GetLastEventProcessedForHandlers();
        }

        private  void GetLastEventProcessedForHandlers()
        {
            var positions = new List<Position>();
            _eventHandlers.ForEach(x => positions.Add(x.LastProcessedPosition.Position)); 
            position = positions.FirstOrDefault(p => p.CommitPosition == positions.Min(c => c.CommitPosition));
        }

        public void RegisterHandlers()
        {
            // add all handlers to the broadcast block so they receive news of events
            _broadcastBlock = new BroadcastBlock<IGESEvent>(x => x);
            // pass the two methods of the handler to the block, one determines if we are interested in this event, the other processes it.
            _eventHandlers.ForEach(x => _broadcastBlock.LinkTo(x.ReturnActionBlock(), y => x.Handles.ContainsKey(y.GetType())));
            _broadcastBlock.LinkTo(DataflowBlock.NullTarget<IGESEvent>());
        }

        public void StartDispatching()
        {
            _logger.LogInfo("Dispatcher started: {0}",DateTime.Now);
            _stopRequested = false;
            //ok we might need to make GESEvent an other dreaded baseclass because I want handlers to know 
            // if the stream is live or catchup.  and the only way to convey that is on the event it's self
            _subscription = _gesConnection.SubscribeToAllFrom(position, false, HandleNewEvent, x=>_isLive=true, null, new UserCredentials("admin", "changeit"));
        }

        public void StopDispatching()
        {
            _logger.LogInfo("Dispatcher stopped: {0}", DateTime.Now);
            _stopRequested = true;
            if (_subscription != null)
                _subscription.Stop(TimeSpan.FromSeconds(2));
        }

        private void HandleNewEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent @event)
        {
            if (!_eventFilter(@event)) { return; }
            // make sure the event fits the ones this handler cares about
            var _event = @event.ProcessRawEvent(_targetTypeName);
            // filter for null event ( didn't have metadata or data )
            if (_event == null) { return; }
            HandleEvent(_event);
        }

        protected void HandleEvent(IGESEvent _event)
        {
            _logger.LogInfo("Broadcasting event: {0}", _event.EventType);
            _broadcastBlock.Post(_event);
        }
    }
}