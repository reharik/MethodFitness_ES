using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class UserArchived : GESEvent
    {
        public Guid UserId { get; private set; }
        public DateTime ArchivedDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public UserArchived(Guid userId, DateTime archivedDate)
        {
            UserId = userId;
            ArchivedDate = archivedDate;
            EventType = GetType().Name;
        }
    }

    public class UserUnArchived : GESEvent
    {
        public Guid UserId { get; private set; }
        public DateTime UnArchivedDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public UserUnArchived(Guid userId, DateTime unArchivedDate)
        {
            UserId = userId;
            UnArchivedDate = unArchivedDate;
            EventType = GetType().Name;
        }
    }
}