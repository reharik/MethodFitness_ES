using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class ArchiveClient : GESEvent
    {
        public ArchiveClient(Guid clientId)
        {
            ClientId = clientId;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
    }

    public class UnArchiveClient : GESEvent
    {
        public UnArchiveClient(Guid clientId)
        {
            this.ClientId = clientId;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
    }
}