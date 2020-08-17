using System;
using System.Collections;
using Sample.Foundation.Logging.Services;
using Sitecore.Caching;
using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Diagnostics;

namespace Sample.Foundation.Caching.Events
{
    public class SitecoreCacheClearer
    {
        public void ClearCaches(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            try
            {
                DoClear();
            }
            catch (Exception ex)
            {
                var logService = ServiceLocator.ServiceProvider.GetService<ILogService>();
                logService.Error(string.Format("Error clearing cache. Exception was: {0}", ex));
            }
        }

        private void DoClear()
        {
            foreach (string cacheName in Caches)
            {
                Cache cache = (Cache)CacheManager.FindCacheByName(cacheName);
                if (cache == null)
                    continue;

                var logService = ServiceLocator.ServiceProvider.GetService<ILogService>();
                logService.Info(string.Format("Clearing {0} items from {1} {2}.", cache.Count, cacheName, this));
                cache.Clear();
            }
        }

        public ArrayList Caches { get; } = new ArrayList();
    }
}