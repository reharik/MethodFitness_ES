using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.MessageBinders
{
     public class MessageBinderRegistry : Registry
    {
        public MessageBinderRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });

        }
    }
}