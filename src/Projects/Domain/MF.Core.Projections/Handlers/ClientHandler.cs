using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class ClientHandler : HandlerBase, IHandler
    {
        public ClientHandler(IMongoRepository mongoRepository, IUIResponsePoster uiResponsePoster)
            : base(mongoRepository,uiResponsePoster)
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
            client.Id = clientSignedUp.Id.ToString();
            client.Contact = clientSignedUp.Contact;
            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);
            _responseMessage = new UINotification("Success", "Success");
        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new Clients();
            client.Id = clientSignedUp.Id.ToString();
            client.Contact = clientSignedUp.Contact;
            //TODO this should be populated ... somewhere higher up.
//            client.Source = clientSignedUp.Source;
            client.SourceNotes = clientSignedUp.SourceNotes;
            _mongoRepository.Save(client);
            _responseMessage = new UINotification("Success", "Success");
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