using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class BookHalfHourSession : IGESEvent
    {
        public Guid Id { get; private set; }
        public string Location { get; private set; }
        public TrainerDisplay TrainerDisplay { get; set; }
        public ClientDisplay ClientDisplay { get; set; }
        public DateTime AppointmentDate { get; private set; }
        public string StartTime { get; private set; }
        public string Notes { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public BookHalfHourSession(string location,
                                   TrainerDisplay trainerDisplay,
                                   ClientDisplay clientDisplay,
                                   DateTime appointmentDate,
                                   string startTime,
                                   string notes)
        {
            Location = location;
            TrainerDisplay = trainerDisplay;
            ClientDisplay = clientDisplay;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            Notes = notes;
            EventType = GetType().Name;
        }
    }
}