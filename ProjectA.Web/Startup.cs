using Microsoft.Owin;
using Owin;
using ProjectA.Database;
using ProjectA.Framework.Messaging;
using QSeed;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(ProjectA.Web.Startup))]
namespace ProjectA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new SeedersRunner(typeof(Repository<>), masterSeederType: typeof(DatabaseSeeder))
                .RegisterSeedersAssembly(typeof(Repository<>).Assembly)
                .RegisterFactoriesAssembly(typeof(Repository<>).Assembly)
                .Run();
        }
    }
}
