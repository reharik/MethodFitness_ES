using System.Collections.Generic;
using System.Text;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;

namespace MF.Core.Projections
{

    public class ReadStoreDispatcher : DispatcherBase
    {
        public ReadStoreDispatcher(IGESConnection gesConnection, List<IHandler> eventHandlers)
            : base(gesConnection, eventHandlers)
        {
            _targetTypeName = "EventTypeName";
            _eventFilter = x =>
            {
                if (x.OriginalEvent.Metadata.Length <= 0 || x.OriginalEvent.Data.Length <= 0 || (!_isLive&&x.Event.EventType == "UIProjection"))
                { return false; }
                var jProperty = Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(x.Event.Metadata)).Property(_targetTypeName);
                return !x.Event.EventType.StartsWith("$") && jProperty != null && jProperty.HasValues;
            };
        }
    }
}