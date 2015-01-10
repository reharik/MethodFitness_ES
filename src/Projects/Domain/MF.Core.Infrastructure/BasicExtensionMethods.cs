using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MF.Core.Infrastructure
{
    public static class BasicExtensionMethods
    {
        public static IGESEvent ProcessRawEvent(this ResolvedEvent vent, string targetTypeName)
        {
            if (vent.OriginalEvent.Metadata.Length <= 0 || vent.OriginalEvent.Data.Length <= 0)
            { return null; }

            var gesEvent = DeserializeEvent(vent.OriginalEvent.Metadata, vent.OriginalEvent.Data, targetTypeName);
            gesEvent.OriginalPosition = vent.OriginalPosition;
            return gesEvent;
        }

        private static IGESEvent DeserializeEvent(byte[] metadata, byte[] data, string targetTypeName)
        {
            // tried to get this out of here and into one call but couldn't do it
            var actualTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(targetTypeName);
            //********** Application specific MetaData *************
//            var clientId = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property("ClientId");
//            var clientRoom = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property("ClientRoom");
//            var returnEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property("ReturnEvent");
            var metaData = new Dictionary<string, object>();// {{"ClientRoom", clientRoom}, {"ReturnEvent", returnEvent},{"ClientId",clientId}};
            //********** End Application specific MetaData *************
             var vent = (IGESEvent) JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string) actualTypeName));
            vent.MetaData = metaData;
            return vent;
        }


        public static EventData ToEventData(this IGESEvent vent, IDictionary<string, object> headers, string eventTypeHeader)
        {
            var serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
            var serializeObject = JsonConvert.SerializeObject(vent, serializerSettings);
            var data = Encoding.UTF8.GetBytes(serializeObject);

            headers.Add(eventTypeHeader, vent.GetType().AssemblyQualifiedName);

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers, serializerSettings));
            var typeName = vent.GetType().Name;
            
            return new EventData(Guid.NewGuid(), typeName, true, data, metadata);
        }

        public static Dictionary<string, object> AddDictionary(this Dictionary<string, object> dic, Dictionary<string, object> targetDic)
        {
            return (targetDic ?? new Dictionary<string, object>())
               .Concat(dic)
               .GroupBy(d => d.Key)
               .ToDictionary(d => d.Key, d => d.First().Value);
        }
    }
}