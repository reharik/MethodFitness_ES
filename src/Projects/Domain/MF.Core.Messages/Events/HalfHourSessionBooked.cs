using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class SingleSessionBooked : GESEvent
    {
        public Guid Id { get; protected set; }
        public TrainerDisplay TrainerDisplay { get; set; }
        public ClientDisplay ClientDisplay { get; set; }
        public string Location { get; protected set; }
        public DateTime AppointmentDate { get; protected set; }
        public string StartTime { get; protected set; }
        public string EndTime { get; protected set; }
        public string Notes { get; protected set; }
    }

    public class HalfHourSessionBooked : SingleSessionBooked
    {
        public HalfHourSessionBooked(Guid id,
                                     string location,
                                     TrainerDisplay trainerDisplay,
                                     ClientDisplay clientDisplay,
                                     DateTime appointmentDate,
                                     string startTime,
                                     string endTime,
                                     string notes)
        {
            Id = id;
            Location = location;
            TrainerDisplay = trainerDisplay;
            ClientDisplay = clientDisplay;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
            EventType = GetType().Name;
        }
    }

    public class FullHourSessionBooked : SingleSessionBooked
    {
        public FullHourSessionBooked(Guid id,
                                     string location,
                                     TrainerDisplay trainerDisplay,
                                     ClientDisplay clientDisplay,
                                     DateTime appointmentDate,
                                     string startTime,
                                     string endTime,
                                     string notes)
        {
            Id = id;
            Location = location;
            TrainerDisplay = trainerDisplay;
            ClientDisplay = clientDisplay;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
            EventType = GetType().Name;
        }
    }
}