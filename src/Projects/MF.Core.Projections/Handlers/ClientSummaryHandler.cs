using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class ClientSummaryHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _mongoRepository;

        public ClientSummaryHandler(IMongoRepository mongoRepository,
            IUIResponsePoster uiResponsePoster,
            ILogger logger)
            : base(mongoRepository, uiResponsePoster, logger)
        {
            _mongoRepository = mongoRepository;
            register(typeof(HouseGeneratedClientSignedUp), HandleHouseGenerated);
            register(typeof(TrainerGeneratedClientSignedUp), HandleTrainerGenerated);
            register(typeof(ClientArchived), clientArchived);
            register(typeof(ClientUnArchived), clientUnArchived);
            register(typeof(ClientNameCorrected), clientNameCorrected);
            register(typeof(ClientEmailChanged), clientEmailChanged);
            register(typeof(ClientPhoneChanged), clientPhoneChanged);
        }

        private void clientNameCorrected(IGESEvent x)
        {
            var vent = (ClientNameCorrected)x;
            var client = _mongoRepository.Get<ClientSummaries>(u => u.Id == vent.ClientId.ToString());
            client.FirstName = vent.Contact.FirstName;
            client.LastName = vent.Contact.LastName;
            _mongoRepository.Save(client);
        }

        private void clientEmailChanged(IGESEvent x)
        {
            var vent = (ClientEmailChanged)x;
            var client = _mongoRepository.Get<ClientSummaries>(u => u.Id == vent.ClientId.ToString());
            client.EmailAddress = vent.Contact.EmailAddress;
            _mongoRepository.Save(client);
        }

        private void clientPhoneChanged(IGESEvent x)
        {
            var vent = (ClientPhoneChanged)x;
            var client = _mongoRepository.Get<ClientSummaries>(u => u.Id == vent.ClientId.ToString());
            client.Phone = vent.Contact.Phone;
            _mongoRepository.Save(client);
        }

        private void HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new ClientSummaries();
            client.Id = clientSignedUp.Id.ToString();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.Phone;
            _mongoRepository.Save(client);
        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new ClientSummaries();
            client.Id = clientSignedUp.Id.ToString();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.Phone;
            _mongoRepository.Save(client);
        }

        private void clientArchived(IGESEvent x)
        {
            var clientArchived = (ClientArchived)x;
            var client = _mongoRepository.Get<ClientSummaries>(u => u.Id == clientArchived.ClientId.ToString());
            client.Archived = true;
            client.ArchivedDate = clientArchived.ArchivedDate;
            _mongoRepository.Save(client);
        }

        private void clientUnArchived(IGESEvent x)
        {
            var clientUnArchived = (ClientUnArchived)x;
            var client = _mongoRepository.Get<ClientSummaries>(u => u.Id == clientUnArchived.ClientId.ToString());
            client.Archived = false;
            client.ArchivedDate = clientUnArchived.UnArchivedDate;
            _mongoRepository.Save(client);
        }
    }
}