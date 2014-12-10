using System.Web.Mvc;
using System.Web.Routing;

namespace BookStorage
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}
