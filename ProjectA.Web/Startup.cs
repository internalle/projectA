using Microsoft.Owin;
using Owin;
using ProjectA.Framework.Messaging;
using ProjectA.Services.Database;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(ProjectA.Web.Startup))]
namespace ProjectA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyResolver.Current.GetService<IDispatcher>().Dispatch(new InitializeStaticData.Request());
        }
    }
}
