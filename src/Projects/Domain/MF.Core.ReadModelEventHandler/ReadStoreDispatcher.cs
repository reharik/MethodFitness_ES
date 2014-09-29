using System.Collections.Generic;
using System.Text;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;

namespace MF.Core.ReadModelEventHandler
{

    public class ReadStoreDispatcher : DispatcherBase
    {
        public ReadStoreDispatcher(IMongoRepository mongoRepository, IGESConnection gesConnection, List<IHandler> eventHandlers)
            : base(mongoRepository, gesConnection, eventHandlers)
        {
            _targetClrTypeName = "EventClrTypeName";
            _eventFilter = x =>
            {
                if (x.OriginalEvent.Metadata.Length <= 0 || x.OriginalEvent.Data.Length <= 0)
                { return false; }
                var jProperty = Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(x.Event.Metadata)).Property(_targetClrTypeName);
                return !x.Event.EventType.StartsWith("$") && jProperty != null && jProperty.HasValues;
            };
        }
    }
}