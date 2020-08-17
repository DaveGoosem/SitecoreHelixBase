using Sitecore.Pipelines;
using System.Web.Routing;
using Sitecore;
using Sample.Foundation.Routing.App_Start;

namespace Sample.Foundation.Routing.Pipelines
{
    public class InitializeRoutes
    {
        public void Process(PipelineArgs args)
        {
            if (!Context.IsUnitTesting)
            {
                RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
        }
    }
}