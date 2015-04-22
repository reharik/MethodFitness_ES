using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.Messages.Command
{
    public class ChangeClientPhone : GESEvent
    {
        public ChangeClientPhone(Guid clientId, Contact contact)
        {
            ClientId = clientId;
            Contact = contact;
            EventType = GetType().Name;
        }

        public Guid ClientId { get; private set; }
        public Contact Contact { get; private set; }
    }   
}