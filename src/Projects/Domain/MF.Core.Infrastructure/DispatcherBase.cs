using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System.Linq;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public interface IDispatcher
    {
        void StartDispatching();
        void StopDispatching();
    }

    public class DispatcherBase : IDispatcher
    {
        private readonly List<IHandler> _eventHandlers;
        protected IEventStoreConnection _gesConnection;
        private bool _stopRequested;
        private EventStoreAllCatchUpSubscription _subscription;
        private BroadcastBlock<IGESEvent> _broadcastBlock;
        protected string _targetTypeName;
        protected Func<ResolvedEvent, bool> _eventFilter;
        private Position position;
        private bool _isLive;

        public DispatcherBase(IGESConnection gesConnection, List<IHandler> eventHandlers)
        {
            _gesConnection = gesConnection.BuildConnection();
            _gesConnection.ConnectAsync();
            _eventHandlers = eventHandlers;
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
            _stopRequested = false;
            //ok we might need to make IGESEvent an other dreaded baseclass because I want handlers to know 
            // if the stream is live or catchup.  and the only way to convey that is on the event it's self
            _subscription = _gesConnection.SubscribeToAllFrom(position, false, HandleNewEvent, x=>_isLive=true, null, new UserCredentials("admin", "changeit"));
        }

        public void StopDispatching()
        {
            _stopRequested = true;
            if (_subscription != null)
                _subscription.Stop(TimeSpan.FromSeconds(2));
        }

        private void HandleNewEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent @event)
        {
            // make sure the event fits the ones this handler cares about
            if (!_eventFilter(@event)) { return; }
            var _event = @event.ProcessRawEvent(_targetTypeName);
            // filter for null event ( didn't have metadata or data )
            if (_event == null) { return; }
            HandleEvent(_event);
        }

        protected void HandleEvent(IGESEvent _event)
        {
            _broadcastBlock.Post(_event);
        }
    }
}