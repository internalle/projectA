using Autofac;
using MongoDB.Driver;
using MongoRepository;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.Mongo.Repository;

namespace ProjectA.Configuration.Mongo.DI
{
    public class MongoConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(MongoRepository<,>)).UsingConstructor(typeof(MongoUrl)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(MongoDBRepository<>)).As(typeof(Core.IRepository<>)).InstancePerLifetimeScope();
            builder.Register((container) =>
            {
                var settings = container.Resolve<IAppSettings>();
                return new MongoUrl(settings.MongoDBConnectionString);
            }).InstancePerLifetimeScope();
        }
    }
}
