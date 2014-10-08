using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class HireTrainerWorkflow : WorkflowBase, IHandler
    {
        public HireTrainerWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
            register(typeof(HireTrainer), hireTrainer);
        }

        private User hireTrainer(IGESEvent x)
        {
            var archiveUser = (HireTrainer)x;
            var user = new User();
            user.Handle(archiveUser);
            return user;
        }
    }
}