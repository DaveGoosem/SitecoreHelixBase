using System;
using Sample.Foundation.DependencyInjection;
using Sitecore.Publishing;
using Sample.Foundation.GlassMapper;
using Sitecore.SecurityModel;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Sample.Foundation.SitecoreExtensions.Services
{
    [Service(typeof(IPublishingService))]
    public class PublishingService : IPublishingService
    {
        public void PublishItem(Item item, PublishMode publishMode, bool publishDeep = true, string sourceDatabaseName = Constants.MasterContextName, string targetDatabaseName = Constants.WebContextName, bool publishAsync = true)
        {
            using (new SecurityDisabler())
            {
                // We need to be able to set the source and target databases so that when we run things from another context like scheduler or others, we can publish as desired.
                var sourceDatabase = Database.GetDatabase(sourceDatabaseName);
                var targetDatabase = Database.GetDatabase(targetDatabaseName);
                PublishOptions publishOptions = new PublishOptions(sourceDatabase, targetDatabase, publishMode, item.Language, DateTime.Now);

                publishOptions.UserName = "sitecore\\admin";
                Publisher publisher = new Publisher(publishOptions);

                // Publish start item
                publisher.Options.RootItem = item;

                // set "Deep" to true to Publish the child items as well
                publisher.Options.Deep = publishDeep;

                // Run the publish as async or sync as required
                if (publishAsync)
                    publisher.PublishAsync();
                else
                    publisher.Publish();

                item.Publishing.ClearPublishingCache();
            }
        }
    }
}