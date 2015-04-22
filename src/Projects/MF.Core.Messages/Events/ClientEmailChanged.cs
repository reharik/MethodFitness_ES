﻿using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Infrastructure.SharedModels.CommonDtos;

namespace MF.Core.Messages.Events
{
    public class ClientEmailChanged : GESEvent
    {
        public Guid ClientId { get; private set; }
        public Contact Contact { get; private set; }

        public ClientEmailChanged(Guid clientId, Contact contact)
        {
            ClientId = clientId;
            Contact = contact;
            EventType = GetType().Name;
        }
    }

}