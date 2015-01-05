using System;
using System.Collections.Generic;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public interface IUIResponsePoster
    {
        void PostEvent(IGESEvent @event, Guid commitId, Dictionary<string, object> updateHeaders = null);
    }

    public class UIResponsePoster : MessagePoster, IUIResponsePoster
    {
        public UIResponsePoster(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection)
        {
            CommandTypeHeader = "CommandTypeName";
            EventStreamName = "UIResponse";
        }
    }
}