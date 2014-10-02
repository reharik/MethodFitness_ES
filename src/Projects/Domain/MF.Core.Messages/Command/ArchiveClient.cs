using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class ArchiveClient : IGESEvent
    {
        public ArchiveClient(Guid clientId)
        {
            ClientId = clientId;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }

    public class UnArchiveClient : IGESEvent
    {
        public UnArchiveClient(Guid clientId)
        {
            this.ClientId = clientId;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}