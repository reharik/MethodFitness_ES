using MF.Core.Infrastructure;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.Domain
{
    public class DomainRegistry : Registry
    {
        public    DomainRegistry()
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