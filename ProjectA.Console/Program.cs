using Autofac;
using Autofac.Extras.CommonServiceLocator;
using FluentNHibernate.Automapping;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.DI;
using ProjectA.Database;
using Qmand;
using QSeed;
using QSeed.Model;
using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectA.Configuration.Base.Types;

namespace ProjectA.Console
{
    public class Program
    {
        public class cfg : IAppSettings
        {
            public string ConnectionString => "Server=localhost; Port=3306; Database=ProjectA; Uid=root; Pwd=;";
            public DatabaseType Database => DatabaseType.MySQL;
            public bool IsDebug => true;
        }
        private static void BootDI()
        {
            var builder = new ContainerBuilder();
            builder = AutofacConfiguration.Load(builder, new cfg());
            var container = builder.Build();
            
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            var config = ServiceLocator.Current.GetInstance<NHibernate.Cfg.Configuration>();
            ServiceLocator.Current.GetInstance<AutoPersistenceModel>().Configure(config);
        }

        static void Main(string[] args)
        {
            BootDI();
            CommandMarshal.RegisterCommandAssembly(typeof(Program).Assembly);

            while (true)
            {
                var line = System.Console.ReadLine();
                CommandMarshal.ExecuteCommandString(line);
            }
        }
    }
}
