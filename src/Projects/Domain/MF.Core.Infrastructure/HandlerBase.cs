using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public class HandlerBase
    {
        protected readonly IMongoRepository _mongoRepository;
        protected string _handlerType;
        private LastProcessedPosition _lastProcessedPosition;
        public Dictionary<Type, Action<IGESEvent>> Handles { get; set; }

        public HandlerBase(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;

            _handlerType = GetType().Name;
            Handles = new Dictionary<Type, Action<IGESEvent>>();
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x => HandleEvent(x, Handles[x.GetType()]));
        }

        protected void register(Type t, Action<IGESEvent> func)
        {
            Handles.Add(t, func);
        }

        protected virtual void HandleEvent(IGESEvent @event, Action<IGESEvent> handleBy)
        {
            try
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(@event)) { return; }
                
                handleBy(@event);

                SetEventAsRecorded(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //TODO Publish a message with this error
            }
            finally
            {
                //TODO Possibly publish message
            }
        }

        public bool ExpectEventPositionIsGreaterThanLastRecorded(IGESEvent x)
        {
            return x.OriginalPosition == null || LastProcessedPosition.CommitPosition >= x.OriginalPosition.Value.CommitPosition;
        }

        public void SetEventAsRecorded(IGESEvent @event)
        {
            if (!@event.OriginalPosition.HasValue)
                throw new ArgumentException("ResolvedEvent didn't come off a subscription to all (has no position).");

            LastProcessedPosition.CommitPosition = @event.OriginalPosition.Value.CommitPosition;
            LastProcessedPosition.PreparePosition = @event.OriginalPosition.Value.PreparePosition;
            _mongoRepository.Save(LastProcessedPosition);
        }

        public LastProcessedPosition LastProcessedPosition
        {
            get
            {
                if (_lastProcessedPosition == null)
                {
                    _lastProcessedPosition = _mongoRepository.Get<LastProcessedPosition>(x => x.HandlerType == _handlerType)
                                             ?? new LastProcessedPosition
                                                 {
                                                     CommitPosition = Position.Start.CommitPosition,
                                                     PreparePosition = Position.Start.PreparePosition,
                                                     HandlerType = _handlerType
                                                 };
                }
                return _lastProcessedPosition;
            }
        }
    }
}