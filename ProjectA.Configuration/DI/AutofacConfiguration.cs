using Autofac;
using Autofac.Core;
using NHibernate;
using ProjectA.Configuration;
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
        public static ContainerBuilder Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();
            builder.RegisterType<DatabaseLogger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<Dispatcher>().Named("dispatcher", typeof(IDispatcher)).InstancePerRequest();

            builder
                .RegisterAssemblyTypes(typeof(BaseHandler<,>).Assembly)
                .Where(x => x.IsClosedTypeOf(typeof(BaseHandler<,>)))
                .As(x => x.BaseType)
                .InstancePerRequest();
            builder
                .RegisterAssemblyTypes(typeof(BaseValidator<,>).Assembly)
                .Where(x => x.IsClosedTypeOf(typeof(BaseValidator<,>)))
                .As(x => x.BaseType)
                .InstancePerRequest();

            builder.RegisterDecorator<IDispatcher>((x, inner) => new ExceptionLogger(inner, x.Resolve<ILogger>()), "dispatcher").InstancePerRequest();
            builder.RegisterAssemblyModules(typeof(AutofacConfiguration).Assembly);
            
            return builder;
        }
    }
}
