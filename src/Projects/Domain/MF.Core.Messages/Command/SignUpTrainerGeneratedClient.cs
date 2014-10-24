using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class SignUpTrainerGeneratedClient : IGESEvent
    {
        public SignUpTrainerGeneratedClient(
            Contact contact,
            Address address,
            Guid trainerId,
            string sourceNotes,
            DateTime startDate)
        {
            Contact = contact;
            Address = address;
            TrainerId = trainerId;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            EventType = GetType().Name;
        }

        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Guid TrainerId { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}