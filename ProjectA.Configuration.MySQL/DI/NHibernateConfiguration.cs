﻿using Autofac;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System.Reflection;
using FluentNHibernate.Conventions.Instances;
using ProjectA.Core;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.NHibernateORM.Repository;

namespace ProjectA.Configuration.NHibernateORM.DI
{
    public class NHibernateConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(NHibernateRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.Register((container) =>
            {
                var configuration = container.Resolve<NHibernate.Cfg.Configuration>();
                var model = BuildPersistenceModel(configuration);
                model.Configure(configuration);
                return model;
            }).As<AutoPersistenceModel>().SingleInstance().AutoActivate();
        }

        private NHibernate.Cfg.Configuration BuildConfiguration(IAppSettings appSettings)
        {
            var config = Fluently.Configure()
                .Database(SelectDatabaseType(appSettings))
                .ExposeConfiguration(c => c.SetProperty(Environment.ReleaseConnections, "on_close"))
                .ExposeConfiguration(c => c.SetProperty(Environment.ProxyFactoryFactoryClass, typeof(NHibernate.Bytecode.DefaultProxyFactoryFactory).AssemblyQualifiedName))
                .ExposeConfiguration(c => c.SetProperty(Environment.Hbm2ddlAuto, "update"))
                //.ExposeConfiguration(c => c.SetProperty(Environment.ShowSql, "true"))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();

            if (config == null)
                throw new System.Exception("Cannot build NHibernate configuration");

            return config;
        }

        private IPersistenceConfigurer SelectDatabaseType(IAppSettings settings)
        {
            if(settings.Database == Base.Enums.DatabaseType.MySQL)
            {
                return MySQLConfiguration.Standard.ConnectionString(settings.MySqlConnectionString);
            }
            else if (settings.Database == Base.Enums.DatabaseType.MsSQL)
            {
                //return MsSqlConfiguration.MsSql7.ConnectionString(settings.MySqlConnectionString);
            }
            else if (settings.Database == Base.Enums.DatabaseType.PostgreSQL)
            {
                //return PostgreSQLConfiguration.Standard.ConnectionString(settings.MySqlConnectionString);
            }

            return null;
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
            persistenceModel.Configure(config);

            //persistenceModel.WriteMappingsTo(@"C:\Users\vojda\Desktop\ProjectA");

            return persistenceModel;
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            //new SchemaExport(config).SetOutputFile(Path.Combine(@"C:\Users\vojda\Desktop\ProjectA", @"Schema.sql")).Create(true, true);
        }
    }
}