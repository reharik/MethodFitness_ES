using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class BootstrapApplication:IGESEvent
    {
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }

        public BootstrapApplication()
        {
            EventType = GetType().Name;
        }
    }
}