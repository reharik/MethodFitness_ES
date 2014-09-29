using EventStore.ClientAPI;

namespace MF.Core.Infrastructure.SharedModels
{
    public interface IGESEvent
    {
        string EventType { get; }
        Position? OriginalPosition { get; set; }
    }
}