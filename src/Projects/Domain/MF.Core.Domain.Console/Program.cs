using System;
using MF.Core.Infrastructure;
using StructureMap;

namespace MF.Core.Domain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();
            try
            {
                var dispatcher = ObjectFactory.Container.GetInstance<IDispatcher>();
                dispatcher.StartDispatching();
            }
            catch (Exception ex)
            {
                // do something with exception.  emit event or something
                System.Console.WriteLine(ex.Message);
            }
            System.Console.Read();
        }   
    }
}
