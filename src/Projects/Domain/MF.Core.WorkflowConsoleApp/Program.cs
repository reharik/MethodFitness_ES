﻿using System;
using EventSpike.Infrastructure;
using StructureMap;

namespace EventSpike.WorkflowConsoleApp
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
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }   
    }
}
