using MF.Core.Infrastructure;
using MF.Core.ReadModel;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.Projections
{
    public class ProjectionsRegistry : Registry
    {
        public ProjectionsRegistry()
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