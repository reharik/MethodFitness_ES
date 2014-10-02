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
    public class ArchiveClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public ArchiveClientWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == typeof(ArchiveClient).Name;
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(async x =>
                {
                    if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; }
                    var archiveClient = (ArchiveClient)x;

                    Client client = await _getEventStoreRepository.GetById<Client>(archiveClient.ClientId);
                    client.Handle(archiveClient);
                    _getEventStoreRepository.Save(client, Guid.NewGuid());
                
                    SetEventAsRecorded(x);
                });
        }
    }
}