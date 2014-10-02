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
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType == typeof(HouseGeneratedClientSignedUp).Name) { return true; }
            if (@event.EventType == typeof(TrainerGeneratedClientSignedUp).Name) { return true; }
            if (@event.EventType == typeof(ClientArchived).Name) { return true; }
            if (@event.EventType == typeof(ClientUnArchived).Name) { return true; } 
            return false;
        } 
       
        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
                {
                    switch (x.EventType)
                    {
                        case "HouseGeneratedClientSignedUp":
                            HandleEvent(x, HandleHouseGenerated);
                            break;
                        case "TrainerGeneratedClientSignedUp":
                            HandleEvent(x, HandleTrainerGenerated);
                            break;
                        case "ClientArchived":
                            HandleEvent(x, clientArchived);
                            break;
                        case "ClientUnArchived":
                            HandleEvent(x, clientUnArchived);
                            break;
                    }
                }, new ExecutionDataflowBlockOptions()
                {
                    MaxDegreeOfParallelism = 4
                });
        }

        private IReadModel HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new Client();
            client.Id = clientSignedUp.Id;
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            return client;
        }

        private IReadModel HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new Client();
            client.Id = clientSignedUp.Id;
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            //TODO this should be populated ... somewhere higher up.
//            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            return client;
        }

        private IReadModel clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _mongoRepository.Get<Client>(u => u.Id == clientArchived.ClientId);
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            return client;
        }

        private IReadModel clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _mongoRepository.Get<Client>(u => u.Id == clientUnArchived.ClientId);
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            return client;
        }
    }
}