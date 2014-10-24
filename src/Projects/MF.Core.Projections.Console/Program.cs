using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MF.Core.Infrastructure;
using StructureMap;

namespace MF.Core.Projections.Console
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
