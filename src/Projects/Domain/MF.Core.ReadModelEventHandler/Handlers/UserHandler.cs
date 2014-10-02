using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.ReadModelEventHandler.Handlers
{
    public class UserHandler : HandlerBase, IHandler
    {
        public UserHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == typeof(TrainerHired).Name) { return true; }
            if (@event.EventType == typeof(UserLoggedIn).Name) { return true; }
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
                            HandleEvent(x,trainerHired);
                            break;
                        case "UserLoggedIn":
                            HandleEvent(x,userLoggedIn);
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

        private IReadModel userLoggedIn(IGESEvent x)
        {
            var userLoggedIn = (UserLoggedIn) x;
            var userLogins = new UserLogins
                {
                    UserName = userLoggedIn.UserName,
                    Id = userLoggedIn.Id,
                    Token = userLoggedIn.Token,
                    Date = userLoggedIn.Now
                };
            return userLogins;
        }

        private IReadModel trainerHired(IGESEvent x)
        {
            var trainerHired = (TrainerHired)x;
            var user = new User();
            user.Id = trainerHired.Id;
            user.UserName = trainerHired.Credentials.UserName;
            user.FirstName = trainerHired.Contact.FirstName;
            user.LastName = trainerHired.Contact.LastName;
            user.EmailAddress = trainerHired.Contact.EmailAddress;
            user.Address1 = trainerHired.Address.Address1;
            user.Address2 = trainerHired.Address.Address2;
            user.City = trainerHired.Address.City;
            user.State = trainerHired.Address.State;
            user.ZipCode = trainerHired.Address.ZipCode;
            user.PhoneMobile = trainerHired.Contact.PhoneMobile;
            user.PhoneSecondary = trainerHired.Contact.PhoneSecondary;
            user.Dob = trainerHired.Dob;
            return user;
        }

        private IReadModel userArchived(IGESEvent x)
        {
            var userArchived = (UserArchived)x;
            var user = _mongoRepository.Get<User>(u => u.Id == userArchived.UserId);
            user.Archived = true;
            user.ArchivedDate = userArchived.ArchivedDate;
            return user;
        }

        private IReadModel userUnArchived(IGESEvent x)
        {
            var userUnArchived = (UserUnArchived)x;
            var user = _mongoRepository.Get<User>(u => u.Id == userUnArchived.UserId);
            user.Archived = false;
            user.ArchivedDate = userUnArchived.UnArchivedDate;
            return user;
        }
    }
}