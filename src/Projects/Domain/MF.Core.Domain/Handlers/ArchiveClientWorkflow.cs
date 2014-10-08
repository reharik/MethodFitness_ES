using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class ArchiveClientWorkflow : WorkflowBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public ArchiveClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
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