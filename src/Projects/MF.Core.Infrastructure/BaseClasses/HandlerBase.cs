using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure.BaseClasses
{
    public class HandlerBase
    {
        protected readonly IMongoRepository _mongoRepository;
        private readonly IUIResponsePoster _uiResponsePoster;
        private readonly ILogger _logger;
        protected string _handlerType;
        private LastProcessedPosition _lastProcessedPosition;
        public Dictionary<Type, Action<IGESEvent>> Handles { get; set; }
        public GESEvent _responseMessage;
        protected string _continuationId;

        public HandlerBase(IMongoRepository mongoRepository, IUIResponsePoster uiResponsePoster, ILogger logger)
        {
            _mongoRepository = mongoRepository;
            _uiResponsePoster = uiResponsePoster;
            _logger = logger;

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
                _logger.LogInfo("Processing Event: {0}", @event.EventType);
                _responseMessage = new UINotification("Success", "Success", @event.EventType);
                _continuationId = @event.MetaData["ContinuationId"] != null ? @event.MetaData["ContinuationId"].ToString() : "";
                
                handleBy(@event);

                _logger.LogInfo("Event {0} processed", @event.EventType);
                SetEventAsRecorded(@event);

            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message, ex);
                _responseMessage = new UINotification("Failure", ex.Message, @event.EventType);
            }
            finally
            {
                if (_responseMessage != null)
                {
                    _uiResponsePoster.PostEvent(_responseMessage, Guid.NewGuid(), new Dictionary<string, object> { { "ContinuationId", _continuationId } });
                }
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