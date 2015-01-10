using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.ReadModel;
using StructureMap;
using StructureMap.Graph;

namespace MF.Core.Projections.Console
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
                x.For<IDispatcher>().Use<ReadStoreDispatcher>();
                x.AddRegistry(new InfrastructureRegistry());
                x.AddRegistry(new ReadModelRegistry());
                x.AddRegistry(new ProjectionsRegistry());
            });
//            ObjectFactory.Container.AssertConfigurationIsValid();

        }
    }
}