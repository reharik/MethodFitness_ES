using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class UserArchived : IGESEvent
    {
        public Guid UserId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public UserArchived(Guid userId)
        {
            UserId = userId;
            EventType = GetType().Name;
        }
    }

    public class UserUnArchived : IGESEvent
    {
        public Guid UserId { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public UserUnArchived(Guid userId)
        {
            UserId = userId;
            EventType = GetType().Name;
        }
    }
}