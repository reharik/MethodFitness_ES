using System;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public class WorkflowBase : HandlerBase
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public WorkflowBase(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
        }

        protected override void HandleEvent(IGESEvent @event, Func<IGESEvent, object> handleBy)
        {
            try
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(@event)) { return; }
                var view = handleBy(@event);
                // some events don't need you to save, so they will return null; bit smelly
                if (view != null) { _getEventStoreRepository.Save((IAggregate)view, Guid.NewGuid()); }

                SetEventAsRecorded(@event);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //TODO Publish a command failed message
            }
            finally
            {
                //TODO Possibly publish message
            }
        }
    }
}