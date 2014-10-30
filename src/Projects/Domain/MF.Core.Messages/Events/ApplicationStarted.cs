using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ApplicationStarted:GESEvent
    {
        public ApplicationStarted()
        {
            EventType = GetType().Name;
        }
    }
}