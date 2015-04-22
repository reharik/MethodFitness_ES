using System;
using MF.Core.Infrastructure.BaseClasses;
using Topshelf;

namespace MF.Service.Projections
{
    public interface IProjectionsServiceControl : ServiceControl
    {
    }

    public class ProjectionsServiceControl : IProjectionsServiceControl
    {
        private readonly IDispatcher _dispatcher;

        public ProjectionsServiceControl(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                _dispatcher.StartDispatching();
            }
            catch (Exception ex)
            {
                // do something with exception.  emit event or something
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _dispatcher.StopDispatching();
            return true;
        }
    }
}