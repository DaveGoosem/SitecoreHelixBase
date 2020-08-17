using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;


namespace Sample.Foundation.SitecoreExtensions.Extensions
{
    public static class SiteExtensions
    {    
        public static Item GetContextItem(this SiteContext site, ID derivedFromTemplateID)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            var startItem = site.GetStartItem();
            return startItem?.GetAncestorOrSelfOfTemplate(derivedFromTemplateID);
        }

        public static Item GetRootItem(this SiteContext site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            return site.Database.GetItem(site.RootPath);
        }

        public static Item GetStartItem(this SiteContext site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            return site.Database.GetItem(site.StartPath);
        }

        public static Item GetTenantItem(this SiteContext site)
        {
            var startItem = site.GetStartItem();
            return startItem?.Parent;
        }
    }
}