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
    public class ArchiveUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public ArchiveUserWorkflow(IMongoRepository mongoRepository, 
            IGetEventStoreRepository getEventStoreRepository,
            IUIResponsePoster uiResponsePoster,
            ILogger logger)
            : base(mongoRepository, uiResponsePoster, logger)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(ArchiveUser), archiveUser);
        }

        private void archiveUser(IGESEvent x)
        {
            var archiveUser = (ArchiveUser)x;
            var user = _getEventStoreRepository.GetById<User>(archiveUser.TrainerId).Result;
            user.Handle(archiveUser);
            _getEventStoreRepository.Save(user, Guid.NewGuid(), _continuationId);
        }
    }
}