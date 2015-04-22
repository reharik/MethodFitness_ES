using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.Messages.Command
{
    public class UpdateClientSourceNotes : GESEvent
    {
        public UpdateClientSourceNotes(Guid clientId,string sourceNotes)
        {
            ClientId = clientId;
            SourceNotes = sourceNotes;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
        public string SourceNotes { get; private set; }
    }   
}