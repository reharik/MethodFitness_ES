using System;
using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
//    public class ArchiveClientWorkflow : HandlerBase, IHandler
//    {
//        private readonly IGetEventStoreRepository _getEventStoreRepository;
//        public ArchiveClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
//            : base(mongoRepository)
//        {
//            _getEventStoreRepository = getEventStoreRepository;
//            register(typeof(ArchiveClient),archiveClient);
//        }
//
//        private void archiveClient(IGESEvent x)
//        {
//            var archiveClient = (ArchiveClient) x;
//            var client = _getEventStoreRepository.GetById<Client>(archiveClient.ClientId).Result;
//            client.Handle(archiveClient);
//            _getEventStoreRepository.Save(client, Guid.NewGuid());
//        }
//    }
}