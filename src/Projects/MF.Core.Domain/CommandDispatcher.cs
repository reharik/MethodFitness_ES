﻿using System.Collections.Generic;
using System.Text;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.GES.Interfaces;

namespace MF.Core.Domain
{
    public class CommandDispatcher : DispatcherBase
    {
        public CommandDispatcher(IGESConnection gesConnection, List<IHandler> eventHandlers, IUIResponsePoster uiResponsePoster, ILogger logger)
            : base(gesConnection, eventHandlers, uiResponsePoster, logger)
        {
            _targetTypeName = "CommandTypeName";

            _eventFilter = x =>
                {
                    if (x.OriginalEvent.Metadata.Length <= 0 || x.OriginalEvent.Data.Length <= 0)
                    { return false; }
                    var jProperty = Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(x.Event.Metadata)).Property(_targetTypeName);
                    return !x.Event.EventType.StartsWith("$") && jProperty!=null && jProperty.HasValues;
                };
        }
    }
}