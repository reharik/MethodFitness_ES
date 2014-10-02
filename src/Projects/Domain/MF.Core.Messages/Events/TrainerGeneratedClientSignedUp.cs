using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class TrainerGeneratedClientSignedUp : IGESEvent
    {
        private readonly Contact _contact;
        public Guid Id { get; private set; }
        public Contact Contact { get; set; }
        public Guid TrainerId { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public TrainerGeneratedClientSignedUp(Guid id,
          Contact contact,
            Guid trainerId,
            string sourceNotes,
            DateTime startDate)
        {
            _contact = contact;
            Id = id;
           
            TrainerId = trainerId;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            EventType = GetType().Name;
        }

    }
}