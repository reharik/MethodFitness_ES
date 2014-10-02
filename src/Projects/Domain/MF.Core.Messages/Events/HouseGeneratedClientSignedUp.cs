using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class HouseGeneratedClientSignedUp : IGESEvent
    {
        public Guid Id { get; private set; }
        public Contact Contact { get; set; }
        public Guid TrainerId { get; private set; }
        public string Source { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public HouseGeneratedClientSignedUp(Guid id,
         Contact contact,
            Guid trainerId,
            string source,
            string sourceNotes,
            DateTime startDate)
        {
            Id = id;
            Contact = contact;
            TrainerId = trainerId;
            Source = source;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            EventType = GetType().Name;
        }

    }
}