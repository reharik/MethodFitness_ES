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
    public class HireTrainerWorkflow : HandlerBase, IHandler
    {
        public HireTrainerWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
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