using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ApplicationStarted:GESEvent
    {
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public ApplicationStarted()
        {
            EventType = GetType().Name;
        }
    }
}