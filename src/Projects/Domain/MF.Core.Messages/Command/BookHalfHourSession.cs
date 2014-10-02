using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class BookHalfHourSession : IGESEvent
    {
        public Guid Id { get; private set; }
        public string Location { get; private set; }
        public Guid TrainerId { get; private set; }
        public string TrainerName { get; private set; }
        public Guid ClientId { get; set; }
        public Guid ClieentId { get; private set; }
        public string ClientName { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public string StartTime { get; private set; }
        public string Notes { get; private set; }

        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public BookHalfHourSession(string location,
                                   Guid trainerId,
                                   string trainerName,
                                   Guid clientId,
                                   string clientName,
                                   DateTime appointmentDate,
                                   string startTime,
                                   string notes)
        {
            Location = location;
            TrainerId = trainerId;
            TrainerName = trainerName;
            ClientId = clientId;
            ClientName = clientName;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            Notes = notes;
            EventType = GetType().Name;
        }
    }
}