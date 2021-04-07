using Sample.Feature.Analytics.Models;
using Sample.Foundation.DependencyInjection;
using Sample.Foundation.GlassMapper.Services;

namespace Sample.Feature.Analytics.Services
{
    [Service(typeof(IAnalyticsService))]
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IGlassMapperService _glassMapperService;
        public AnalyticsService(IGlassMapperService glassMapperService)
        {
            _glassMapperService = glassMapperService;
        }


        public ICommonScripts GetCommonScripts()
        {
            return _glassMapperService.GetTenantItem<ICommonScripts>();
        }
    }
}