using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ClientCreated :IGESEvent
    {
        public Guid Id { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public ClientCreated(Guid id)
        {
            Id = id;
            EventType = "ClientCreated";
        }
    }
}