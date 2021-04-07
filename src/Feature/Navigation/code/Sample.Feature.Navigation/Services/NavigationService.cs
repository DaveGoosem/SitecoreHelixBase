using Sample.Feature.Navigation.Models;
using Sample.Foundation.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;

namespace Sample.Feature.Navigation.Services
{
    [Service(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        public IEnumerable<INavigationItem> GetNavigationItems(INavigationItem startItem, int depthLevel, int maxDepthLevel)
        {
            if (startItem == null || depthLevel > maxDepthLevel || startItem.Children.Count() == 0)
            {
                return null;
            }

            var childItems = startItem.Children.Where(x => !IsLocalDatasourceFolderItem(x)).Select(x => x);
            if (childItems != null)
                return childItems;

            return null;
        }

        
        private bool IsLocalDatasourceFolderItem(INavigationItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return item.TemplateId.Equals(ID.Parse(Sample.Foundation.LocalDatasource.Settings.LocalDatasourceFolderTemplate));
        }

    }
}