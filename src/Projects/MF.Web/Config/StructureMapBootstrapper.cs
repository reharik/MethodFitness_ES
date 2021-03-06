using MF.Web.Config;
using StructureMap;

namespace MF.Web.Config
{
    public class StructureMapBootstrapper : IBootstrapper
    {
        private static bool _hasStarted;

        public virtual void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new MFWebRegistry());
                                         });
            ObjectFactory.Container.AssertConfigurationIsValid();
        }

        public static void Restart()
        {
            if (!_hasStarted)
            {
                Bootstrap();
                _hasStarted = true;
            }
        }

        public static void Bootstrap()
        {
            new StructureMapBootstrapper().BootstrapStructureMap();
        }
    }

    public class StructureMapBootstrapperTesting : IBootstrapper
    {
        private static bool _hasStarted;

        public virtual void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new MFTestRegistry());
            });
        }


        public static void Restart()
        {
            if (!_hasStarted)
            {
                Bootstrap();
                _hasStarted = true;
            }
        }

        public static void Bootstrap()
        {
            new StructureMapBootstrapperTesting().BootstrapStructureMap();
        }
    }
}