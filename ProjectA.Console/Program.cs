using Autofac;
using Autofac.Extras.CommonServiceLocator;
using FluentNHibernate.Automapping;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.DI;
using Qmand;
using ProjectA.Configuration.Base.Enums;

namespace ProjectA.Console
{
    public class Program
    {
        private static void BootDI()
        {
            ConfigurationBootstraper.Load(new ContainerBuilder(), new AppSettings());
        }

        static void Main(string[] args)
        {
            BootDI();
            var marshal = new CommandMarshal();
            marshal.RegisterCommandAssembly(typeof(Program).Assembly);

            while (true)
            {
                var line = System.Console.ReadLine();
                marshal.ExecuteCommandString(line);
            }
        }
    }
}
