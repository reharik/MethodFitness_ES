using System;
using System.Threading.Tasks;
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
        public UnArchiveClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(UnArchiveClient), archiveClient);
        }

        private async Task<Client> archiveClient(IGESEvent x)
        {
            var vent = (UnArchiveClient)x;
            var item = await _getEventStoreRepository.GetById<Client>(vent.ClientId);
            item.Handle(vent);
            return item;
        }
    }
}