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
    public class ArchiveClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public ArchiveClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository) : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(ArchiveClient),archiveClient);
        }

        private async Task<Client> archiveClient(IGESEvent x)
        {
            var archiveClient = (ArchiveClient) x;
            Client client = await _getEventStoreRepository.GetById<Client>(archiveClient.ClientId);
            client.Handle(archiveClient);
            return client;
        }
    }
}