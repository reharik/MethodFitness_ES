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
    public class ClientHandler : HandlerBase, IHandler
    {
        public ClientHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _handlerType = "ClientHandler";
            _lastProcessedPosition = new LastProcessedPosition();
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == "ClientCreated") { return true; }
            if (@event.EventType == "NewClientSignedUp") { return true; }
            return false;
        } 
       
        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
                {
                    switch (x.EventType)
                    {
                        case "ClientCreated":
                            HandleEvent(x,clientCreated);
                            break;
                        case "NewClientSignedUp":
                            HandleEvent(x, newClientSignedUp);
                            break;
                    }
                }, new ExecutionDataflowBlockOptions()
                {
                    MaxDegreeOfParallelism = 4
                });
        }

        private IReadModel newClientSignedUp(IGESEvent x)
        {
            Thread.Sleep(1000);
            var trainerHired = (TrainerHired)x;
            var client = _mongoRepository.Get<Client>(u => u.Id == trainerHired.Id);
            client.FirstName = trainerHired.FirstName;
            client.LastName = trainerHired.LastName;
            client.EmailAddress = trainerHired.EmailAddress;
            client.Address1 = trainerHired.Address1;
            client.Address2 = trainerHired.Address2;
            client.City = trainerHired.City;
            client.State = trainerHired.State;
            client.ZipCode = trainerHired.ZipCode;
            client.PhoneMobile = trainerHired.PhoneMobile;
            client.PhoneSecondary = trainerHired.PhoneSecondary;
            return client;
        }

        private IReadModel clientCreated(IGESEvent x)
        {
            var clientCreated = (ClientCreated)x;
            var client = new Client {Id = clientCreated.Id};
            return client;
        }
    }
}