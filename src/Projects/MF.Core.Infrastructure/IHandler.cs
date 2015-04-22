using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public interface IHandler
    {
        ActionBlock<IGESEvent> ReturnActionBlock();
        Dictionary<Type, Action<IGESEvent>> Handles { get; set; }
        LastProcessedPosition LastProcessedPosition { get; }
    }
}