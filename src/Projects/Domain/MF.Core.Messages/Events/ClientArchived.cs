using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ClientArchived : GESEvent
    {
        public Guid ClientId { get; private set; }
        public DateTime ArchivedDate { get; private set; }

        public ClientArchived(Guid clientId, DateTime archivedDate)
        {
            ClientId = clientId;
            ArchivedDate = archivedDate;
            EventType = GetType().Name;
        }
    }

    public class ClientUnArchived : GESEvent
    {
        public Guid ClientId { get; private set; }
        public DateTime UnArchivedDate { get; private set; }

        public ClientUnArchived(Guid clientId, DateTime unArchivedDate)
        {
            ClientId = clientId;
            UnArchivedDate = unArchivedDate;
            EventType = GetType().Name;
        }
    }
}