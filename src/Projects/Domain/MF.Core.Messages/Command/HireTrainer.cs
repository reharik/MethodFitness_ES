using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class HireTrainer : IGESEvent
    {
        public HireTrainer(
         Contact contact,
            Credentials credentials,
            Address address,
            DateTime dob)
        {
            Contact = contact;
            Credentials = credentials;
            Address = address;
            Dob = dob;
            EventType = GetType().Name;
        }

        public Contact Contact { get; set; }
        public Credentials Credentials { get; set; }
        public Address Address { get; set; }
        public DateTime Dob { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}