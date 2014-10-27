using System;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;

namespace MF.Core.Projections.Handlers
{
    public class UIResponseHandler : HandlerBase, IHandler
    {
        private readonly IMongoRepository _repository;
        private readonly IUIResponsePoster _uiResponsePoster;

        public UIResponseHandler(IMongoRepository repository, IUIResponsePoster uiResponsePoster)
            : base(repository, uiResponsePoster)
        {
            _repository = repository;
            _uiResponsePoster = uiResponsePoster;
            register(typeof (HouseGeneratedClientSignedUp), HandleHouseGenerated);
            register(typeof (TrainerGeneratedClientSignedUp), HandleTrainerGenerated);
        }

        private void HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp) x;
        }

        private void HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp) x;
            var
                client = new ClientSummaries();
            client.FirstName = clientSignedUp.Contact.FirstName;
            client.LastName = clientSignedUp.Contact.LastName;
            client.EmailAddress = clientSignedUp.Contact.EmailAddress;
            client.Phone = clientSignedUp.Contact.Phone;
            _mongoRepository.Save(client);
        }
    }
}