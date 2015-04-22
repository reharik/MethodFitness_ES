using System;
using MF.Core.Infrastructure.BaseClasses;
using StructureMap;
using Topshelf;

namespace MF.Service.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            HostFactory.Run(x =>                                 
            {
                x.Service(ObjectFactory.GetInstance<IDomainServiceControl>);
                x.RunAsLocalSystem();                            

                x.SetDescription("Domain Service");
                x.SetDisplayName("MF.DomainService");
                x.SetServiceName("MF.DomainService");                       
            });
        }

        private static void Initialize()
        {
            Bootstrapper.Bootstrap();
        }   
    }
}
