using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.ReadModel
{
     public class ReadModelRegistry : Registry
    {
        public ReadModelRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
          
        }
    }
}