using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.Messages.Events
{
    public class ClientSourceNotesUpdated : GESEvent
    {
        public Guid ClientId { get; private set; }
        public string SourceNotes { get; private set; }

        public ClientSourceNotesUpdated(Guid clientId, string sourceNotes)
        {
            ClientId = clientId;
            SourceNotes = sourceNotes;
            EventType = GetType().Name;
        }
    }

}