using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Messages.Command;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class SignUpTrainerGeneratedClientMessageBinder : MessageBinderBase
    {
        private readonly IMongoRepository _mongoRepository;

        public SignUpTrainerGeneratedClientMessageBinder(IMongoRepository mongoRepository, IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
            _mongoRepository = mongoRepository;
        }

        public void AcceptRequest(string firstName,
            string lastName,
            string emailAddress,
            string phone,
            Guid trainerId,
            string sourceNotes,
            DateTime startDate)
        {
            var user = _mongoRepository.Get<Client>(x => x.EmailAddress == emailAddress);
            if (user != null)
            {
                throw new Exception("Client with that email address already exists");
            }

            // validate email address.
            var signUpNewClient = new SignUpTrainerGeneratedClient(
                firstName, 
                lastName, 
                emailAddress, 
                phone,
                trainerId,
                sourceNotes,
                startDate);
            PostEvent(signUpNewClient, Guid.NewGuid());

        }
    }
}