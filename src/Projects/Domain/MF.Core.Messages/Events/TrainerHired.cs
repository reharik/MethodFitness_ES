using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class TrainerHired : GESEvent
    {
        public Guid Id { get; private set; }
        public Address Address { get; private set; }
        public Contact Contact { get; private set; }
        public Credentials Credentials { get; private set; }
        public DateTime Dob { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public TrainerHired(Guid id,
           Credentials credentials,
            Contact contact,
            Address address,
            DateTime dob)
        {
            Id = id;
            Credentials = credentials;
            Contact = contact;
            Address = address;
            Dob = dob;
            EventType = GetType().Name;
        }
    }
}