using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ClientArchived : IGESEvent
    {
        public Guid ClientId { get; private set; }
        public DateTime ArchivedDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public ClientArchived(Guid clientId, DateTime archivedDate)
        {
            ClientId = clientId;
            ArchivedDate = archivedDate;
            EventType = GetType().Name;
        }
    }

    public class ClientUnArchived : IGESEvent
    {
        public Guid ClientId { get; private set; }
        public DateTime UnArchivedDate { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public ClientUnArchived(Guid clientId, DateTime unArchivedDate)
        {
            ClientId = clientId;
            UnArchivedDate = unArchivedDate;
            EventType = GetType().Name;
        }
    }
}