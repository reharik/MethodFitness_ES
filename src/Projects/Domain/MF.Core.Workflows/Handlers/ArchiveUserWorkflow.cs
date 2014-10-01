using System;
using System.Threading.Tasks.Dataflow;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;
using Newtonsoft.Json;

namespace MF.Core.Workflows.Handlers
{
    public class ArchiveUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public ArchiveUserWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == typeof(ArchiveUser).Name;
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(async x =>
                {
                    if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; }
                    var archiveUser = (ArchiveUser) x;

                    User user = await _getEventStoreRepository.GetById<User>(archiveUser.TrainerId);
                    user.Handle(archiveUser);
                    _getEventStoreRepository.Save(user, Guid.NewGuid());
                
                    SetEventAsRecorded(x);
                });
        }
    }
}