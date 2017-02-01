using Autofac;
using ProjectA.Framework.Logging;
using ProjectA.Framework.Messaging;
using ProjectA.Framework.Messaging.Decorators;
using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.DI.Framework.Messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Dispatcher>().Named("dispatcher", typeof(IDispatcher)).InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(BaseHandler<,>).Assembly)
                .Where(x => x.IsClosedTypeOf(typeof(BaseHandler<,>)))
                .As(x => x.BaseType)
                .InstancePerLifetimeScope();
            builder
                .RegisterAssemblyTypes(typeof(BaseValidator<,>).Assembly)
                .Where(x => x.IsClosedTypeOf(typeof(BaseValidator<,>)))
                .As(x => x.BaseType)
                .InstancePerLifetimeScope();

            // Decorator
            builder.RegisterDecorator<IDispatcher>((x, inner) => new ExceptionLogger(inner, x.Resolve<ILogger>()), "dispatcher").InstancePerLifetimeScope();
        }
    }
}
