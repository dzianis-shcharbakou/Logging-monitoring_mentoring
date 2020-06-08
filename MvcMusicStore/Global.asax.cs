using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastructure;
using NLog;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly NLog.ILogger logger;

        public MvcApplication()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.Register(f => LogManager.GetLogger("ForControllers")).
                As<NLog.ILogger>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            logger.Info("Application started");

            PerformanceCounterCreator.CreateAllCounters();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            logger.Error(ex.ToString());
        }
    }
}
