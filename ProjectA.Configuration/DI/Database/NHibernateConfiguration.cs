using Autofac;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ProjectA.Core;
using System.IO;
using System.Reflection;
using FluentNHibernate.Conventions.Instances;

namespace ProjectA.Configuration.DI.Database
{
    public class NHibernateConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerDependency();
            builder.Register((container) =>
            {
                var settings = container.Resolve<IAppSettings>();
                return BuildConfiguration(settings);
            }).As<NHibernate.Cfg.Configuration>().SingleInstance();

            builder.Register((container) =>
            {
                var configuration = container.Resolve<NHibernate.Cfg.Configuration>();
                return BuildSessionFactory(configuration);
            }).As<ISessionFactory>().SingleInstance();

            builder.Register((container) =>
            {
                var configuration = container.Resolve<NHibernate.Cfg.Configuration>();
                return BuildPersistenceModel(configuration);
            }).As<AutoPersistenceModel>().SingleInstance();
        }

        private NHibernate.Cfg.Configuration BuildConfiguration(IAppSettings appSettings)
        {
            var config = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(appSettings.ConnectionString))
                .ExposeConfiguration(c => c.SetProperty(Environment.ReleaseConnections, "on_close"))
                .ExposeConfiguration(c => c.SetProperty(Environment.ProxyFactoryFactoryClass, typeof(NHibernate.Bytecode.DefaultProxyFactoryFactory).AssemblyQualifiedName))
                .ExposeConfiguration(c => c.SetProperty(Environment.Hbm2ddlAuto, "create"))
                .ExposeConfiguration(c => c.SetProperty(Environment.ShowSql, "true"))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();

            if (config == null)
                throw new System.Exception("Cannot build NHibernate configuration");

            return config;
        }

        private ISessionFactory BuildSessionFactory(NHibernate.Cfg.Configuration config)
        {
            var sessionFactory = config.BuildSessionFactory();

            if (sessionFactory == null)
                throw new System.Exception("Cannot build NHibernate Session Factory");

            return sessionFactory;
        }

        private AutoPersistenceModel BuildPersistenceModel(NHibernate.Cfg.Configuration config)
        {
            var persistenceModel = AutoMap.AssemblyOf<Entity>();

            persistenceModel.Conventions.Setup(c =>
            {
                //c.Add(typeof(ForeignKeyNameConvention));
                //c.Add(typeof(ReferenceConvention));
                //c.Add(typeof(PrimaryKeyNameConvention));
                //c.Add(typeof(TableNameConvention));
            });

            persistenceModel.UseOverridesFromAssembly(typeof(Entity).Assembly);

            //persistenceModel.WriteMappingsTo(@"C:\Users\vojda\Desktop\ProjectA");

            return persistenceModel;
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            //new SchemaExport(config).SetOutputFile(Path.Combine(@"C:\Users\vojda\Desktop\ProjectA", @"Schema.sql")).Create(true, true);
        }
    }
}