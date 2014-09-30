using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.ReadModelEventHandler.Handlers
{
    public class UserSummaryHandler : HandlerBase, IHandler
    {
        public UserSummaryHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _handlerType = "UserSummaryHandler";
            _lastProcessedPosition = new LastProcessedPosition();
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == "TrainerHired") { return true; }
            return false;
        } 
       
        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x => HandleEvent(x,trainerHired), new ExecutionDataflowBlockOptions()
                {
                    MaxDegreeOfParallelism = 4
                });
        }

        private IReadModel trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new UserSummary();
            user.FirstName = trainerHired.FirstName;
            user.LastName = trainerHired.LastName;
            user.EmailAddress = trainerHired.EmailAddress;
            user.PhoneMobile = trainerHired.PhoneMobile;
            return user;
        }
    }
}