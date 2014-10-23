using MF.Core.Infrastructure;
using MF.Core.ReadModel;
using StructureMap;
using StructureMap.Graph;

namespace MF.Core.Domain.Console
{
public class Bootstrapper
    {
        public static void  Bootstrap()
        {
            new Bootstrapper().Start();
        }

        private void Start()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(z =>
                {
                    z.TheCallingAssembly();
                    z.AddAllTypesOf<IHandler>();
                    z.WithDefaultConventions();
                });
                x.For<IDispatcher>().Use<CommandDispatcher>();

                x.AddRegistry(new DomainRegistry());
                x.AddRegistry(new InfrastructureRegistry());
                                x.AddRegistry(new ReadModelRegistry());
            });
//            ObjectFactory.Container.AssertConfigurationIsValid();

        }
    }
}