using System.Collections.Generic;
using EventStore.ClientAPI;

namespace MF.Core.Infrastructure.SharedModels
{
    public interface IGESEvent
    {
        string EventType { get; }
        Position? OriginalPosition { get; set; }
        Dictionary<string, object> MetaData { get; set; }
    }

    public class GESEvent : IGESEvent
    {
        public string EventType { get; protected set; }
        public Position? OriginalPosition { get; set; }
        public Dictionary<string, object> MetaData { get; set; }
    }
}