using Autofac;
using Autofac.Integration.Mvc;
using ProjectA.Configuration;
using ProjectA.Framework.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectA.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers();
            builder = AutofacConfiguration.Load(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            var name = HttpContext.Current.Request.Cookies.Get("_culture")?.Value as string;

            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(name);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }
    }
}
