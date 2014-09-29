using MF.Core.Infrastructure;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.Workflows
{
     public class WorkflowRegistry : Registry
    {
        public WorkflowRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
                x.AddAllTypesOf<IHandler>();
            });
        }
    }
}