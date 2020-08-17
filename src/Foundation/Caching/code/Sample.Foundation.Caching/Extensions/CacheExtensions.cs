namespace Sample.Foundation.Caching.Extensions
{
    public static class CacheExtensions
    {
        public static string GenerateSiteSpecificCacheKey(string cacheKeyBase)
        {
            var siteTenantItem = SitecoreExtensions.Extensions.SiteExtensions.GetTenantItem(Sitecore.Context.Site);
            return $"{siteTenantItem.Name}_{cacheKeyBase}";
        }
    }
}