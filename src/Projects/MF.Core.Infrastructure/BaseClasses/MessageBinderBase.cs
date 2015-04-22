using System;
using System.Collections.Generic;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure.BaseClasses
{
    public class MessagePoster
    {
        protected IEventStoreConnection _eventStoreConnection;
        private const string CommitIdHeader = "CommitId";
        protected  string CommandTypeHeader;
        protected  string EventStreamName;

        public MessagePoster(IEventStoreConnection eventStoreConnection)
        {
            _eventStoreConnection = eventStoreConnection;
            _eventStoreConnection.ConnectAsync();
        }

        public async void PostEvent(IGESEvent @event, Guid commitId, Dictionary<string, object> updateHeaders = null)
        {
            // standard data for metadata portion of persisted event
            var commitHeaders = new Dictionary<string, object>
                {
                    // handy tracking id
                    {CommitIdHeader, commitId}
                };
            // add extra data to metadata portion of presisted event
            commitHeaders = commitHeaders.AddDictionary(updateHeaders);
            
            // process command so they fit the expectations of GES
            var commandToSave = new[] { @event.ToEventData(commitHeaders, CommandTypeHeader) };
            // post to command stream
            await _eventStoreConnection.AppendToStreamAsync(EventStreamName, ExpectedVersion.Any, commandToSave);
        }
    }

    public class MessageBinderBase : MessagePoster
    {
        public MessageBinderBase(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection)
        {
            CommandTypeHeader = "CommandTypeName";
            EventStreamName = "CommandDispatch";
        }
    }
}