using Sample.Feature.Navigation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Feature.Navigation.Services
{
    public interface INavigationService
    {
        IEnumerable<INavigationItem> GetNavigationItems(INavigationItem startItem, int depthLevel, int maxDepthLevel);
    }
}
