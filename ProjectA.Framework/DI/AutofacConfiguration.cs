using Autofac;
using Autofac.Core;
using ProjectA.Configuration;
using ProjectA.Framework.Logging;
using ProjectA.Framework.Messaging;
using ProjectA.Framework.Messaging.Decorators;
using ProjectA.Services;
using MongoDB.Driver;
using MongoRepository;
using System.Configuration;
using System.Linq;

namespace ProjectA.Framework.DI
{
    public static class AutofacConfiguration
    {
        public static ContainerBuilder Load(ContainerBuilder builder)
        {
            builder.Register<MongoUrl>((container) => new MongoUrl(ConfigurationManager.AppSettings["MongoDBConnectionString"]));
            builder.RegisterGeneric(typeof(MongoRepository<>)).AsSelf().InstancePerRequest();
            builder.RegisterType<AppSettings>().As<IAppSettings>().InstancePerRequest();
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

            return builder;
        }
    }
}
