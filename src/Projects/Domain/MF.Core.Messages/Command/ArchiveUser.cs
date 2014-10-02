using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class ArchiveUser : IGESEvent
    {
        public ArchiveUser(Guid trainerId, DateTime archiveDate)
        {
            TrainerId = trainerId;
            ArchiveDate = archiveDate;
            EventType = GetType().Name;
        }

        public Guid TrainerId { get; private set; }
        public DateTime ArchiveDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }

    public class UnArchiveUser : IGESEvent
    {
        public UnArchiveUser(Guid trainerId, DateTime unArchiveDate)
        {
            TrainerId = trainerId;
            UnArchiveDate = unArchiveDate;
            EventType = GetType().Name;
        }

        public Guid TrainerId { get; private set; }
        public DateTime UnArchiveDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}