using Sample.Foundation.Routing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace Sample.Foundation.Routing.Providers
{
    public class PublicRouteProvider : DefaultDirectRouteProvider
    {
        private readonly string _routePrefix;

        public PublicRouteProvider(string routePrefix)
        {
            if (routePrefix == null) throw new ArgumentNullException(nameof(routePrefix));
            _routePrefix = routePrefix;
        }

        //Applies RoutePrefix attribute to all Controllers
        protected override string GetRoutePrefix(ControllerDescriptor controllerDescriptor)
        {
            return $"{_routePrefix}/{controllerDescriptor.ControllerName}";
        }

        //Treats the action as if it is decorated with Route attribute
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(ActionDescriptor actionDescriptor)
        {
            var publiclyRoutable = actionDescriptor.GetCustomAttributes(typeof(PublicRouteAttribute), false).Any();
            return publiclyRoutable ? new[] { new RouteAttribute(actionDescriptor.ActionName) } : null;
        }

        //Treats the controller as if it is decorated with Route attribute
        protected override IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(ControllerDescriptor controllerDescriptor)
        {
            var publiclyRoutable = controllerDescriptor.GetCustomAttributes(typeof(PublicRouteAttribute), false).Any();
            return publiclyRoutable ? new[] { new RouteAttribute("{action}") } : null;
        }
    }
}