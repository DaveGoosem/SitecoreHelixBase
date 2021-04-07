using System.Web;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Data.Items;
using Sample.Foundation.SitecoreExtensions.Extensions;

namespace Sample.Feature.Analytics.Handlers
{
    public class RobotsHandler : HttpRequestProcessor
    {        
        public override void Process(HttpRequestArgs args)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return;
            }

            string requestUrl = context.Request.Url.ToString();

            if (string.IsNullOrEmpty(requestUrl) || !requestUrl.ToLower().EndsWith("robots.txt"))
            {
                return;
            }

            string robotsContent = string.Empty;

            if (Sitecore.Context.Site != null && Sitecore.Context.Database != null)
            {
                Item siteTenantItem = SiteExtensions.GetTenantItem(Sitecore.Context.Site);
                if(siteTenantItem != null)
                {
                    robotsContent = siteTenantItem.Fields[Constants.RobotsTextField].Value;       
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(robotsContent);
            context.Response.End();
        }
    }
}