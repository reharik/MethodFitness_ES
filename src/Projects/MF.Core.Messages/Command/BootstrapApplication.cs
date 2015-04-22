using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages.Command
{
    public class BootstrapApplication:GESEvent
    {
        public BootstrapApplication()
        {
            EventType = GetType().Name;
        }
    }
}