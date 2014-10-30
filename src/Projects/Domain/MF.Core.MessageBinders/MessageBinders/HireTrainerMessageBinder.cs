using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels.CommonDtos;
using MF.Core.Messages.Command;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class HireTrainerMessageBinder : MessageBinderBase
    {
        private readonly IMongoRepository _mongoRepository;

        public HireTrainerMessageBinder(IMongoRepository mongoRepository, IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
            _mongoRepository = mongoRepository;
        }

        public void AcceptRequest(string userName,
            string password,
            string firstName,
            string lastName,
            string emailAddress,
            string address1,
            string address2,
            string city,
            string state,
            string zipCode,
            string phoneMobile,
            string phoneSecondary,
            DateTime dob)
        {
            var user = _mongoRepository.Get<Users>(x => x.UserName == userName);
            if (user != null)
            {
                throw new Exception("User with that username already exists");
            }

            // validate email address.
            var hireTrainer = new HireTrainer(new Contact(firstName, lastName, emailAddress, phoneMobile, phoneSecondary),
                new Credentials(userName,password),
                new Address(address1,address2,city,state,zipCode), 
                dob);
            PostEvent(hireTrainer, Guid.NewGuid());

        }
    }
}