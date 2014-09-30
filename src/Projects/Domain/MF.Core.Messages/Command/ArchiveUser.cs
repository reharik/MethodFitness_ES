using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class ArchiveUser : IGESEvent
    {
        public ArchiveUser(Guid trainerId)
        {
            TrainerId = trainerId;
            EventType = "ArchiveUser";
        }

        public Guid TrainerId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }

    public class UnArchiveUser : IGESEvent
    {
        public UnArchiveUser(Guid trainerId)
        {
            TrainerId = trainerId;
            EventType = "UnArchiveUser";
        }

        public Guid TrainerId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}