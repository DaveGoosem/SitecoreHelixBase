using Sample.Foundation.Logging.Services;
using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Sample.Feature.Analytics.Services;
using Sample.Foundation.Caching.Services;

namespace Sample.Feature.Analytics.Extensions
{
    public static class AnalyticsExtensions
    {
        readonly static ILogService _logService;
        readonly static IAnalyticsService _analyticsService;

        static AnalyticsExtensions()
        {
            _logService = ServiceLocator.ServiceProvider.GetService<ILogService>();
            _analyticsService = ServiceLocator.ServiceProvider.GetService<IAnalyticsService>();
        }

        /// <summary>
        /// Get body scripts
        /// </summary>
        /// <returns>Contents of multi-line text field as a string</returns>
        public static string GetBodyScripts()
        {
            try
            {                
                if (CacheManagerService.GetObject(Constants.BodyScriptsCacheKey) == null)
                {
                    var commonScripts = _analyticsService.GetCommonScripts();
                    if (commonScripts != null)
                    {
                        CacheManagerService.SetObject(Constants.BodyScriptsCacheKey, commonScripts.BodyScripts);
                        return commonScripts.BodyScripts;
                    }                        
                }
                return CacheManagerService.GetObject(Constants.BodyScriptsCacheKey).ToString();
            }
            catch (Exception ex)
            {
                _logService.Error("Sample.Feature.Analytics -> GetBodyScripts -> Exception Occured", ex);                
            }
            return string.Empty;
        }

        /// <summary>
        /// Get head scripts
        /// </summary>
        /// <returns>Contents of multi-line text field as a string</returns>
        public static string GetHeadScripts()
        {
            try
            {
                //check if we have this value cached for the current site or not
                if (CacheManagerService.GetObject(Constants.HeadScriptsCacheKey) == null)
                {
                    //if it wasn't previously cached and there's a value present, lets set it in cache for next time
                    var commonScripts = _analyticsService.GetCommonScripts();
                    if (commonScripts != null)
                    {
                        CacheManagerService.SetObject(Constants.HeadScriptsCacheKey, commonScripts.HeadScripts);
                        return commonScripts.HeadScripts;
                    }
                }
                //grab the value from cache for the current site
                return CacheManagerService.GetObject(Constants.HeadScriptsCacheKey).ToString();
            }
            catch (Exception ex)
            {
                _logService.Error("Sample.Feature.Analytics -> GetHeadScripts -> Exception Occured", ex);
            }
            return string.Empty;
        }

    }
}