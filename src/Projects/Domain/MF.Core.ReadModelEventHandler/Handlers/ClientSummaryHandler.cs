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
    public class ClientSummaryHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _repository;

        public ClientSummaryHandler(IMongoRepository repository)
            : base(repository)
        {
            _repository = repository;
            register(typeof(HouseGeneratedClientSignedUp), HandleHouseGenerated);
            register(typeof(TrainerGeneratedClientSignedUp), HandleTrainerGenerated);
            register(typeof(ClientArchived), clientArchived);
            register(typeof(ClientUnArchived), clientUnArchived);
        }

        private IReadModel HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new ClientSummary();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            return client;
        }

        private IReadModel HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var 
                client = new ClientSummary();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            return client;
        }

        private IReadModel clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _repository.Get<ClientSummary>(u => u.Id == clientArchived.ClientId);
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            return client;
        }

        private IReadModel clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _repository.Get<ClientSummary>(u => u.Id == clientUnArchived.ClientId);
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            return client;
        }
    }
}