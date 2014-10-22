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
    public class UnArchiveClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public UnArchiveClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(UnArchiveClient), archiveClient);
        }

        private void archiveClient(IGESEvent x)
        {
            var vent = (UnArchiveClient)x;
            var item = _getEventStoreRepository.GetById<Client>(vent.ClientId).Result;
            item.Handle(vent);
            _getEventStoreRepository.Save(item, Guid.NewGuid());
        }
    }
}