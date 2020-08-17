using Sample.Foundation.Routing.Providers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sample.Foundation.Routing.App_Start
{
    public class RouteConfig
    {
        //Register the PublicRouteProvider with MVC
        public static void RegisterRoutes(RouteCollection routes)
        {
            //when applied with data attribute for public route, you can use a path like /endeavourapi/{controller}/{action}
            routes.MapMvcAttributeRoutes(new PublicRouteProvider("g8api"));
        }
    }
}