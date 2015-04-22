using System;
using MF.Core.Infrastructure.BaseClasses;
using StructureMap;
using Topshelf;

namespace MF.Service.Projections
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            HostFactory.Run(x =>
            {
                x.Service(ObjectFactory.GetInstance<IProjectionsServiceControl>);
                x.RunAsLocalSystem();

                x.SetDescription("Projections Service");
                x.SetDisplayName("MF.ProjectionsService");
                x.SetServiceName("MF.ProjectionsService");
            });
        }

        private static void Initialize()
        {
            Bootstrapper.Bootstrap();
        }
    }
}
