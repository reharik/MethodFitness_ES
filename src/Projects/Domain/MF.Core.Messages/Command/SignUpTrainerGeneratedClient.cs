using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class SignUpTrainerGeneratedClient : GESEvent
    {
        public SignUpTrainerGeneratedClient(
            Contact contact,
            Address address,
            Guid trainerId,
            string sourceNotes,
            DateTime startDate,
            DateTime dob)
        {
            Contact = contact;
            Address = address;
            TrainerId = trainerId;
            SourceNotes = sourceNotes;
            StartDate = startDate;
            Dob = dob;
            EventType = GetType().Name;
        }

        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Guid TrainerId { get; private set; }
        public string SourceNotes { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime Dob { get; private set; }
    }
}