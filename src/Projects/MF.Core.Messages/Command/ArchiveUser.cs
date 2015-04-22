using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class ArchiveUser : GESEvent
    {
        public ArchiveUser(Guid trainerId, DateTime archiveDate)
        {
            TrainerId = trainerId;
            ArchiveDate = archiveDate;
            EventType = GetType().Name;
        }

        public Guid TrainerId { get; private set; }
        public DateTime ArchiveDate { get; private set; }
    }

    public class UnArchiveUser : GESEvent
    {
        public UnArchiveUser(Guid trainerId, DateTime unArchiveDate)
        {
            TrainerId = trainerId;
            UnArchiveDate = unArchiveDate;
            EventType = GetType().Name;
        }

        public Guid TrainerId { get; private set; }
        public DateTime UnArchiveDate { get; private set; }
    }
}