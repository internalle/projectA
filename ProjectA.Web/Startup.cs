using Microsoft.Owin;
using Owin;
using ProjectA.Framework.Messaging;
using ProjectA.Seeders;
using ProjectA.Seeders.Features.Measurements;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(ProjectA.Web.Startup))]
namespace ProjectA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new SeedersRunner()
                .RegisterAssembly(typeof(SeedersRunner).Assembly)
                //.RegisterSeeder(typeof(MeasuremetUnitSeeder))
                .Run();
        }
    }
}
