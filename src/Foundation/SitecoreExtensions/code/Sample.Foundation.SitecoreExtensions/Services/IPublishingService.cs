using Sitecore.Publishing;
using Sample.Foundation.GlassMapper;
using Sitecore.Data.Items;

namespace Sample.Foundation.SitecoreExtensions.Services
{
    public interface IPublishingService
    {
        void PublishItem(Item item, PublishMode publishMode, bool publishDeep = true, string sourceDatabaseName = Constants.MasterContextName, string targetDatabaseName = Constants.WebContextName, bool publishAsync = true);
    }
}
