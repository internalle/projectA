using Autofac;
using ProjectA.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.DI.Framework.Logging
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseLogger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}
