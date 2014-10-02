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
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == typeof(TrainerHired).Name) { return true; }
            if (@event.EventType == typeof(UserArchived).Name) { return true; }
            if (@event.EventType == typeof(UserUnArchived).Name) { return true; }
            return false;
        } 
       
        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
            {
                switch (x.EventType)
                {
                    case "TrainerHired":
                        HandleEvent(x, trainerHired);
                        break;
                    case "UserArchived":
                        HandleEvent(x, userArchived);
                        break;
                    case "UserUnArchived":
                        HandleEvent(x, userUnArchived);
                        break;
                }
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 4
            });
        }

        private IReadModel trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new UserSummary();
            user.FirstName = trainerHired.Contact.FirstName;
            user.LastName = trainerHired.Contact.LastName;
            user.EmailAddress = trainerHired.Contact.EmailAddress;
            user.PhoneMobile = trainerHired.Contact.PhoneMobile;
            return user;
        }

        private IReadModel userArchived(IGESEvent x)
        {
            var userArchived = (UserArchived)x;
            var user = _mongoRepository.Get<UserSummary>(u => u.Id == userArchived.UserId);
            user.Archived = true;
            user.ArchivedDate = userArchived.ArchivedDate;
            return user;
        }

        private IReadModel userUnArchived(IGESEvent x)
        {
            var userUnArchived = (UserUnArchived)x;
            var user = _mongoRepository.Get<UserSummary>(u => u.Id == userUnArchived.UserId);
            user.Archived = false;
            user.ArchivedDate = userUnArchived.UnArchivedDate;
            return user;
        }
    }
}