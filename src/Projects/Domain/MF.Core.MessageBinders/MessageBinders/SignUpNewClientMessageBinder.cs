using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Messages.Command;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class SignUpNewClientMessageBinder : MessageBinderBase
    {
        private readonly IMongoRepository _mongoRepository;

        public SignUpNewClientMessageBinder(IMongoRepository mongoRepository, IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
            _mongoRepository = mongoRepository;
        }

        public void AcceptRequest(string firstName,
            string lastName,
            string emailAddress,
            string address1,
            string address2,
            string city,
            string state,
            string zipCode,
            string phoneMobile,
            string phoneSecondary,
            string source,
            string sourceNotes,
            DateTime startDate)
        {
            var user = _mongoRepository.Get<Client>(x => x.EmailAddress == emailAddress);
            if (user != null)
            {
                throw new Exception("Client with that email address already exists");
            }

            // validate email address.
            var signUpNewClient = new SignUpNewClient(
                firstName, 
                lastName, 
                emailAddress, 
                address1,
                address2,
                city,
                state,
                zipCode,
                phoneMobile,
                phoneSecondary,
                source,
                sourceNotes,
                startDate);
            PostEvent(signUpNewClient, Guid.NewGuid());

        }
    }
}