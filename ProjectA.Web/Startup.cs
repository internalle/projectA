using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectA.Web.Startup))]
namespace ProjectA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
