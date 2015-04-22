using System;
using System.Collections.Generic;
using EventStore.ClientAPI;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public string EventType { get; protected set; }
        [JsonIgnore]
        public Position? OriginalPosition { get; set; }
        [JsonIgnore]
        public Dictionary<string, object> MetaData { get; set; }
    }
}