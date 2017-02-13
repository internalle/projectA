using Autofac;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.DI;

namespace ProjectA.UnitTests
{
    public abstract class BaseTest
    {
        private static void BootDI()
        {
            ConfigurationBootstraper.Load(new ContainerBuilder(), new AppSettings());
        }

        public BaseTest()
        {
            BootDI();
        }
    }
}
