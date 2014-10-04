using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public interface IHandler
    {
        ActionBlock<IGESEvent> ReturnActionBlock();
        void GetLastPositionProcessed();
        Dictionary<Type, Func<IGESEvent, object>> Handles { get; set; }
        LastProcessedPosition LPP { get; set; }
    }
}