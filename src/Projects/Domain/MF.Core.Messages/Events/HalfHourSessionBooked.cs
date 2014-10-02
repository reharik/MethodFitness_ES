using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class HalfHourSessionBooked : IGESEvent
    {
        public Guid Id { get; private set; }
        public TrainerDisplay TrainerDisplay { get; set; }
        public ClientDisplay ClientDisplay { get; set; }
        public string Location { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public string StartTime { get; private set; }
        public string Notes { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public HalfHourSessionBooked(Guid id,
                                     string location,
                                     TrainerDisplay trainerDisplay,
                                     ClientDisplay clientDisplay,
                                     DateTime appointmentDate,
                                     string startTime,
                                     string notes)
        {
            Id = id;
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