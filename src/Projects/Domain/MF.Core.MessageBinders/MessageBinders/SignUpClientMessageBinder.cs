using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Messages.Command;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class SignUpClientMessageBinder : MessageBinderBase
    {
        private readonly IMongoRepository _mongoRepository;

        public SignUpClientMessageBinder(IMongoRepository mongoRepository, IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
            _mongoRepository = mongoRepository;
        }

        public void AcceptRequest(string firstName,
            string lastName,
            string emailAddress,
            string phone,
            Guid trainerId,
            string source,
            string sourceNotes,
            DateTime startDate)
        {
            var user = _mongoRepository.Get<Client>(x => x.EmailAddress == emailAddress);
            if (user != null)
            {
                throw new Exception("Client with that email address already exists");
            }

            if (source == "TrainerGenerated")
            {
                var trainerGeneratedClient = new SignUpTrainerGeneratedClient(firstName, lastName, emailAddress, phone, trainerId, sourceNotes, startDate);
                PostEvent(trainerGeneratedClient, Guid.NewGuid());
            }
            else
            {
                // validate email address.
                var houseGeneratedClient = new SignUpHouseGeneratedClient(firstName, lastName, emailAddress, phone, trainerId, source, sourceNotes, startDate);
                PostEvent(houseGeneratedClient, Guid.NewGuid());
            }
        }
    }
}