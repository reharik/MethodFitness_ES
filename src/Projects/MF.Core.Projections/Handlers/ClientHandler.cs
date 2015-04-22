﻿using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class ClientHandler : HandlerBase, IHandler
    {
        public ClientHandler(IMongoRepository mongoRepository,
            IUIResponsePoster uiResponsePoster,
            ILogger logger)
            : base(mongoRepository, uiResponsePoster, logger)
        {
            register(typeof(HouseGeneratedClientSignedUp), HandleHouseGenerated);
            register(typeof(TrainerGeneratedClientSignedUp), HandleTrainerGenerated);
            register(typeof(ClientArchived), clientArchived);
            register(typeof(ClientUnArchived), clientUnArchived);
            register(typeof(ClientNameCorrected), clientNameCorrected);
            register(typeof(ClientEmailChanged), clientEmailChanged);
            register(typeof(ClientPhoneChanged), clientPhoneChanged);
            register(typeof(ClientSourceNotesUpdated), clientSourceNotesUpdated);
        }

        private void clientSourceNotesUpdated(IGESEvent x)
        {
            var vent = (ClientSourceNotesUpdated)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == vent.ClientId.ToString());
            client.SourceNotes = vent.SourceNotes;
            _mongoRepository.Save(client);
        }

        private void clientNameCorrected(IGESEvent x)
        {
            var vent = (ClientNameCorrected)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == vent.ClientId.ToString());
            client.Contact.FirstName = vent.Contact.FirstName;
            client.Contact.LastName = vent.Contact.LastName;
            _mongoRepository.Save(client);  
        }

        private void clientEmailChanged(IGESEvent x)
        {
            var vent = (ClientEmailChanged)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == vent.ClientId.ToString());
            client.Contact.EmailAddress = vent.Contact.EmailAddress;
            _mongoRepository.Save(client);
        }

        private void clientPhoneChanged(IGESEvent x)
        {
            var vent = (ClientPhoneChanged)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == vent.ClientId.ToString());
            client.Contact.Phone = vent.Contact.Phone;
            client.Contact.SecondaryPhone = vent.Contact.SecondaryPhone;
            _mongoRepository.Save(client);
        }

        private void HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new Clients();
            client.Id = clientSignedUp.Id.ToString();
            client.Contact = clientSignedUp.Contact;
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);
        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new Clients();
            client.Id = clientSignedUp.Id.ToString();
            client.Contact = clientSignedUp.Contact;
            //TODO this should be populated ... somewhere higher up.
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);
        }

        private void clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == clientArchived.ClientId.ToString());
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            _mongoRepository.Save(client);
        }

        private void clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == clientUnArchived.ClientId.ToString());
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            _mongoRepository.Save(client);
        }
    }
}