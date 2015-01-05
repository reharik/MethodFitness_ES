using System;
using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class UnArchiveUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public UnArchiveUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository, IUIResponsePoster uiResponsePoster)
            : base(mongoRepository, uiResponsePoster)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(UnArchiveUser), unArchiveUser);
        }

        private void unArchiveUser(IGESEvent x)
        {
            var vent = (UnArchiveUser)x;
            var item = _getEventStoreRepository.GetById<User>(vent.TrainerId).Result;
            item.Handle(vent);
            _getEventStoreRepository.Save(item, Guid.NewGuid());
        }
    }
}