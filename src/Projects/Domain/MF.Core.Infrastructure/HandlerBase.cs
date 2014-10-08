using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using Newtonsoft.Json;

namespace MF.Core.Infrastructure
{
    public class HandlerBase
    {
        protected readonly IMongoRepository _mongoRepository;
        protected string _handlerType;
        public LastProcessedPosition LPP { get; set; }
        public Dictionary<Type, Func<IGESEvent, object>> Handles { get; set; }

        public HandlerBase(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _handlerType = GetType().Name;
            Handles=new Dictionary<Type, Func<IGESEvent, object>>();
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x => HandleEvent(x, Handles[x.GetType()]), 
                new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 4 });
        }

        protected void register(Type t, Func<IGESEvent, object> func)
        {
            Handles.Add(t, func);
        }

        protected virtual void HandleEvent(IGESEvent @event, Func<IGESEvent, object> handleBy)
        {
            try
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(@event)) { return; };
                var view = handleBy(@event);
                // some events don't need you to save, so they will return null; bit smelly
                if (view != null) { _mongoRepository.Save((IReadModel)view); }
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

        #region Event Position Handling
        protected bool ExpectEventPositionIsGreaterThanLastRecorded(IGESEvent x)
        {
            return x.OriginalPosition == null || LPP.CommitPosition >= x.OriginalPosition.Value.CommitPosition;
        }

        protected void SetEventAsRecorded(IGESEvent @event)
        {
            if (!@event.OriginalPosition.HasValue)
                throw new ArgumentException("ResolvedEvent didn't come off a subscription to all (has no position).");
            LPP = _mongoRepository.Get<LastProcessedPosition>(x => x.HandlerType == _handlerType)
                                        ?? new LastProcessedPosition { HandlerType = _handlerType, CommitPosition = 0, PreparePosition = 0 };

            LPP.CommitPosition = @event.OriginalPosition.Value.CommitPosition;
            LPP.PreparePosition = @event.OriginalPosition.Value.PreparePosition;
            _mongoRepository.Save(LPP);
        }

        // this is used by dispatchers on restart
        public void GetLastPositionProcessed()
        {
            LPP = _mongoRepository.Get<LastProcessedPosition>(x => x.HandlerType == _handlerType)
                ?? new LastProcessedPosition { CommitPosition = Position.Start.CommitPosition, PreparePosition = Position.Start.PreparePosition, HandlerType = _handlerType };
        }
        #endregion
    }
}