using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Events
{
    public class ApplicationStarted:IGESEvent
    {
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public ApplicationStarted()
        {
            EventType = GetType().Name;
        }
    }
}