﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        protected string _targetClrTypeName;
        protected Func<ResolvedEvent, bool> _eventFilter;
        private Position position = Position.Start;

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

        private void GetLastEventProcessedForHandlers()
        {
            var actionBlock = new ActionBlock<IHandler>(x => 
                x.GetLastPositionProcessed(), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 8 });
            _eventHandlers.ForEach(async x =>
                {
                    await actionBlock.SendAsync(x);
                    // this calculates the lowest position of all the handlers so we can start subscription from there
                    position = position.CommitPosition > x.LPP.CommitPosition ? x.LPP.Position : position;
                });
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
            _subscription = _gesConnection.SubscribeToAllFrom(position, false, HandleNewEvent, null, null, new UserCredentials("admin", "changeit"));
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
            var _event = ProcessRawEvent(@event);
            // filter for null event ( didn't have metadata or data )
            if (_event == null) { return; }
            HandleEvent(_event);
        }

        public void HandleEvent(IGESEvent _event)
        {
            _broadcastBlock.Post(_event);
        }

        protected IGESEvent ProcessRawEvent(ResolvedEvent @event)
        {
            if (@event.OriginalEvent.Metadata.Length <= 0 || @event.OriginalEvent.Data.Length <= 0)
            { return null; }

            var gesEvent = DeserializeEvent(@event.OriginalEvent.Metadata, @event.OriginalEvent.Data);
            gesEvent.OriginalPosition = @event.OriginalPosition;
            return gesEvent;
        }

        private IGESEvent DeserializeEvent(byte[] metadata, byte[] data)
        {
            // tried to get this out of here and into one call but couldn't do it
            var actualClrTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(_targetClrTypeName);
            return (IGESEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)actualClrTypeName));
        }
    }
}