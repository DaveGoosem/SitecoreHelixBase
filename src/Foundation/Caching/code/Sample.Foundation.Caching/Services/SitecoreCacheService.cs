using Sample.Foundation.Caching.Extensions;
using Sitecore.Caching;

namespace Sample.Foundation.Caching.Services
{
    public class SitecoreCacheService : CustomCache
    {
        public SitecoreCacheService(string name, long maxSize) : base(name, maxSize)
        {
        }

        new public void SetObject(string key, object value)
        {
            key = CacheExtensions.GenerateSiteSpecificCacheKey(key);
            base.SetObject(key, value);
        }

        new public object GetObject(string key)
        {
            key = CacheExtensions.GenerateSiteSpecificCacheKey(key);
            return base.GetObject(key);
        }

        new public void Remove(string key)
        {
            key = CacheExtensions.GenerateSiteSpecificCacheKey(key);
            base.Remove(key);
        }

        new public void Clear()
        {
            base.Clear();
        }
    }
}