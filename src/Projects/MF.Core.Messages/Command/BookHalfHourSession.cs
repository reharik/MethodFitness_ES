using System;
using System.Collections.Generic;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class BookSingleSession : GESEvent
    {
        public Guid Id { get; protected set; }
        public string Location { get; protected set; }
        public TrainerDisplay TrainerDisplay { get; set; }
        public ClientDisplay ClientDisplay { get; set; }
        public DateTime AppointmentDate { get; protected set; }
        public string StartTime { get; protected set; }
        public string Notes { get; protected set; }
    }

    public class BookHalfHourSession : BookSingleSession
    {
        public BookHalfHourSession(Dictionary<string,string> jsonDic)
        {

        }

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

    public class BookFullHourSession : BookSingleSession
    {
        public BookFullHourSession(string location,
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