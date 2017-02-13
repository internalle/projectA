using Autofac;
using Autofac.Core;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Configuration;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.Mongo.DI;
using ProjectA.Configuration.MySQL.DI;
using ProjectA.Core;
using ProjectA.Framework.Logging;
using ProjectA.Framework.Messaging;
using ProjectA.Framework.Messaging.Decorators;
using ProjectA.Services;
using System.Configuration;
using System.Linq;

namespace ProjectA.Configuration.DI
{
    public static class ConfigurationBootstraper
    {
        public static IContainer Load(ContainerBuilder builder, IAppSettings settings)
        {
            builder.RegisterInstance(settings);
            builder.RegisterAssemblyModules(typeof(ConfigurationBootstraper).Assembly);

            if (settings.Database == Base.Enums.DatabaseType.MySQL)
            {
                builder.RegisterAssemblyModules(typeof(MySQLNHibernateConfiguration).Assembly);
            }else if(settings.Database == Base.Enums.DatabaseType.MongoDB)
            {
                builder.RegisterAssemblyModules(typeof(MongoConfiguration).Assembly);
            }

            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            return container;
        }
    }
}
