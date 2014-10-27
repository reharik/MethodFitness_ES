using System;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class HireTrainerWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public HireTrainerWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository, IUIResponsePoster uiResponsePoster)
            : base(mongoRepository, uiResponsePoster)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(HireTrainer), hireTrainer);
        }

        private void hireTrainer(IGESEvent x)
        {
            var archiveUser = (HireTrainer)x;
            var user = new User();
            user.Handle(archiveUser);
            _getEventStoreRepository.Save(user, Guid.NewGuid());
        }
    }
}