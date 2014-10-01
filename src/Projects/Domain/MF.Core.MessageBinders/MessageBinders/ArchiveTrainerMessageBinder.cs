using System;
using EventStore.ClientAPI;
using MF.Core.Infrastructure;
using MF.Core.Messages.Command;

namespace MF.Core.MessageBinders.MessageBinders
{
    public class ArchiveTrainerMessageBinder : MessageBinderBase
    {
        public ArchiveTrainerMessageBinder(IEventStoreConnection eventStoreConnection)
            : base(eventStoreConnection)
        {
        }

        public void AcceptRequest(Guid trainerId, bool archive)
        {
            if (archive)
            {
                var archiveUser = new ArchiveUser(trainerId);
                PostEvent(archiveUser, Guid.NewGuid());
            }
            else
            {
                var unArchiveUser = new UnArchiveUser(trainerId);
                PostEvent(unArchiveUser, Guid.NewGuid());
            }
        }
    }
}