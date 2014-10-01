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
    public class UserHandler : HandlerBase, IHandler
    {
        public UserHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == typeof(UserCreated).Name) { return true; }
            if (@event.EventType == typeof(TrainerHired).Name) { return true; }
            if (@event.EventType == typeof(UserLoggedIn).Name) { return true; }
            return false;
        } 
       
        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
                {
                    switch (x.EventType)
                    {
                        case "UserCreated":
                            HandleEvent(x,userCreated);
                            break;
                        case "TrainerHired":
                            HandleEvent(x,trainerHired);
                            break;
                        case "UserLoggedIn":
                            HandleEvent(x,userLoggedIn);
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
            Thread.Sleep(1000);
            var trainerHired = (TrainerHired)x;
            var user = _mongoRepository.Get<User>(u => u.Id == trainerHired.Id);
            user.UserName = trainerHired.UserName;
            user.FirstName = trainerHired.FirstName;
            user.LastName = trainerHired.LastName;
            user.EmailAddress = trainerHired.EmailAddress;
            user.Address1 = trainerHired.Address1;
            user.Address2 = trainerHired.Address2;
            user.City = trainerHired.City;
            user.State = trainerHired.State;
            user.ZipCode = trainerHired.ZipCode;
            user.PhoneMobile = trainerHired.PhoneMobile;
            user.PhoneSecondary = trainerHired.PhoneSecondary;
            user.Dob = trainerHired.Dob;
            return user;
        }

        private IReadModel userCreated(IGESEvent x)
        {
            var userCreated = (UserCreated)x;
            var user = new User {Id = userCreated.Id};
            return user;
        }
    }
}