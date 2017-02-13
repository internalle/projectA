using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.Mvc;
using FluentNHibernate.Automapping;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Configuration;
using ProjectA.Configuration.Base;
using ProjectA.Configuration.DI;
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
        private void BootDI()
        {
            var container = ConfigurationBootstraper.Load(new ContainerBuilder(), new AppSettings());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_Start()
        {
            BootDI();
            IAppSettings settings = ServiceLocator.Current.GetInstance<IAppSettings>();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles, settings);
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
