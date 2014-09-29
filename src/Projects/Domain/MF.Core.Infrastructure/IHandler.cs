using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Infrastructure
{
    public interface IHandler
    {
        bool HandlesEvent(IGESEvent @event);
        ActionBlock<IGESEvent> ReturnActionBlock();
        void GetLastPositionProcessed();
    }
}