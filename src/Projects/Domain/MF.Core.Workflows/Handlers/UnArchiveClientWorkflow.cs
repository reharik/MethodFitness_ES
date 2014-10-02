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
    public class UnArchiveClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public UnArchiveClientWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == typeof(UnArchiveClient).Name;
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(async x =>
                {
                    if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; }
                    var unArchiveClient = (UnArchiveClient)x;

                    Client client = await _getEventStoreRepository.GetById<Client>(unArchiveClient.ClientId);
                    client.Handle(unArchiveClient);
                    _getEventStoreRepository.Save(client, Guid.NewGuid());
                
                    SetEventAsRecorded(x);
                });
        }
    }
}