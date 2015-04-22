using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.Messages.Events
{
    public class TrainerGeneratedClientSignedUp : GESEvent
    {
        public Guid Id { get; private set; }
        public Contact Contact { get; set; }
        public Guid TrainerId { get; private set; }
        public string Source { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime Dob { get; private set; }

        public TrainerGeneratedClientSignedUp(Guid id,
          Contact contact,
            Guid trainerId,
            string source,
            string sourceNotes,
            DateTime startDate,
            DateTime dob)
        {
            Contact = contact;
            Id = id;

            TrainerId = trainerId;
            Source = source;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            Dob = dob;
            EventType = GetType().Name;
        }

    }
}