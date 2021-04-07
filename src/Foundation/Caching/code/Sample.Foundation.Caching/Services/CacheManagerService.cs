using Sitecore;

namespace Sample.Foundation.Caching.Services
{
    public static class CacheManagerService
    {
        private static readonly SitecoreCacheService _sitecoreCacheService;

        static CacheManagerService()
        {
            //TODO: If we want to use this for multiple caches in the future, maybe we make this CacheName and size params in here..
            _sitecoreCacheService = new SitecoreCacheService(Constants.G8CommonCache, StringUtil.ParseSizeString(Constants.G8CommonCacheSize));
        }

        public static void SetObject(string key, object value)
        {
            _sitecoreCacheService.SetObject(key, value);
        }

        public static object GetObject(string key)
        {
            return _sitecoreCacheService.GetObject(key);
        }

        public static void Remove(string key)
        {
            _sitecoreCacheService.Remove(key);
        }

        public static void Clear()
        {
            _sitecoreCacheService.Clear();
        }
    }
}