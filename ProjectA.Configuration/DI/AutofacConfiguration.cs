using Autofac;
using Autofac.Core;
using ProjectA.Configuration;
using ProjectA.Configuration.Base;
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
    public static class AutofacConfiguration
    {
        public static ContainerBuilder Load(ContainerBuilder builder, IAppSettings settings)
        {
            builder.RegisterInstance(settings);
            builder.RegisterAssemblyModules(typeof(AutofacConfiguration).Assembly);

            if (settings.Database == Base.Types.DatabaseType.MySQL)
            {
                builder.RegisterAssemblyModules(typeof(MySQLNHibernateConfiguration).Assembly);
            }

            return builder;
        }
    }
}
