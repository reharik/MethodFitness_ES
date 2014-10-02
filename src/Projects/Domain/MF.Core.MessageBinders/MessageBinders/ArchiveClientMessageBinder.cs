using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Messages.Command;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class ArchiveClientMessageBinder : MessageBinderBase
    {
        public ArchiveClientMessageBinder(IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
        }

        public void AcceptRequest(Guid clientId, bool archive)
        {
            if (archive)
            {
                var archiveClient = new ArchiveClient(clientId);
                PostEvent(archiveClient, Guid.NewGuid());
            }
            else
            {
                var unArchiveClient = new UnArchiveClient(clientId);
                PostEvent(unArchiveClient, Guid.NewGuid());
            }
        }
    }
}