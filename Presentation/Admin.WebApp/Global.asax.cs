using log4net;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Admin.WebApp.App_Start;
using Admin.Migration;
using Admin.DataService.Interface;

namespace Admin.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger("Admin.WebApp");
        
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            

            

            UnityServiceLocator serviceLocator = new UnityServiceLocator(UnityConfig.GetConfiguredContainer());
            ICultureService cultureService = serviceLocator.GetInstance<ICultureService>();
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            

            RouteConfig.RegisterRoutes(RouteTable.Routes, cultureService);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            
            IMigrationRunner migrationRunner = serviceLocator.GetInstance<IMigrationRunner>();
            migrationRunner.Update();
            
        }
    }
}
