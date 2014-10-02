using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;

namespace MF.Core.Messages.Command
{
    public class LoginUser : IGESEvent
    {
        public LoginUser(Guid id, Credentials credentials)
        {
            Id = id;
            Credentials = credentials;
            EventType = GetType().Name;
        }

        public Guid Id { get; set; }
        public Credentials Credentials { get; set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}