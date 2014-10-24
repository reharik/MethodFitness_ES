using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class ClientHandler : HandlerBase, IHandler
    {
        public ClientHandler(IMongoRepository mongoRepository) : base(mongoRepository)
        {
            register(typeof(HouseGeneratedClientSignedUp), HandleHouseGenerated);
            register(typeof(TrainerGeneratedClientSignedUp), HandleTrainerGenerated);
            register(typeof(ClientArchived), clientArchived);
            register(typeof(ClientUnArchived), clientUnArchived);
        }

        private void HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new Clients();
            client.Id = clientSignedUp.Id;
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.Phone;
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);

        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new Clients();
            client.Id = clientSignedUp.Id;
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.Phone;
            //TODO this should be populated ... somewhere higher up.
//            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);
        }

        private void clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == clientArchived.ClientId);
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            _mongoRepository.Save(client);
        }

        private void clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _mongoRepository.Get<Clients>(u => u.Id == clientUnArchived.ClientId);
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            _mongoRepository.Save(client);
        }
    }
}