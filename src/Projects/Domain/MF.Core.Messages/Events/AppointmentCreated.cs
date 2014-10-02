﻿using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class AppointmentCreated :IGESEvent
    {
        public Guid Id { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public AppointmentCreated(Guid id)
        {
            Id = id;
            EventType = GetType().Name;
        }
    }
}