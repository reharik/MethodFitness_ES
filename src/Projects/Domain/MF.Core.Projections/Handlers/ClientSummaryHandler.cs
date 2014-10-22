﻿using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
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

        private void HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new ClientSummary();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            _mongoRepository.Save(client);
        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var 
                client = new ClientSummary();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.PhoneMobile;
            _mongoRepository.Save(client);
        }

        private void clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _repository.Get<ClientSummary>(u => u.Id == clientArchived.ClientId);
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            _mongoRepository.Save(client);
        }

        private void clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _repository.Get<ClientSummary>(u => u.Id == clientUnArchived.ClientId);
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            _mongoRepository.Save(client);
        }
    }
}