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
            if (@event.EventType == "HouseGeneratedClientSignedUp") { return true; }
            if (@event.EventType == "TrainerGeneratedClientSignedUp") { return true; }
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
                        case "HouseGeneratedClientSignedUp":
                            HandleEvent(x, HandleHouseGenerated);
                            break;
                        case "TrainerGeneratedClientSignedUp":
                            HandleEvent(x, HandleTrainerGenerated);
                            break;
                    }
                }, new ExecutionDataflowBlockOptions()
                {
                    MaxDegreeOfParallelism = 4
                });
        }

        private IReadModel HandleHouseGenerated(IGESEvent x)
        {
            Thread.Sleep(1000);
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = _mongoRepository.Get<Client>(u => u.Id == clientSignedUp.Id);
            client.FirstName = clientSignedUp.FirstName;
            client.LastName = clientSignedUp.LastName;
            client.EmailAddress = clientSignedUp.EmailAddress;
            client.Phone = clientSignedUp.Phone;
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            return client;
        }

        private IReadModel HandleTrainerGenerated(IGESEvent x)
        {
            Thread.Sleep(1000);
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = _mongoRepository.Get<Client>(u => u.Id == clientSignedUp.Id);
            client.FirstName = clientSignedUp.FirstName;
            client.LastName = clientSignedUp.LastName;
            client.EmailAddress = clientSignedUp.EmailAddress;
            client.Phone = clientSignedUp.Phone;
            //TODO this should be populated ... somewhere higher up.
//            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
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