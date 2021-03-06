﻿using System.Collections.Generic;
using System.Text;
using EventSpike.Infrastructure;
using EventSpike.Infrastructure.GES.Interfaces;
using EventSpike.Infrastructure.Mongo;

namespace EventSpike.Workflows
{
    public class CommandDispatcher : DispatcherBase
    {
        public CommandDispatcher(IMongoRepository mongoRepository, IGESConnection gesConnection, List<IHandler> eventHandlers) 
            : base(mongoRepository, gesConnection, eventHandlers)
        {
            _targetClrTypeName = "CommandClrTypeName";
            _eventFilter = x =>
                {
                    if (x.OriginalEvent.Metadata.Length <= 0 || x.OriginalEvent.Data.Length <= 0)
                    { return false; }
                    var jProperty = Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(x.Event.Metadata)).Property(_targetClrTypeName);
                    return !x.Event.EventType.StartsWith("$") && jProperty!=null && jProperty.HasValues;
                };
        }
    }
}