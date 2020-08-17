using System.Collections.Generic;
using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Navigation.Models
{
    public interface INavigationItem : IGlassBase
    {
        string NavTitle { get; set; }
        bool NavDisplayInNav { get; set; }
        bool NavDisplayChildItems { get; set; }
        Link NavRedirectPage { get; set; }

        [SitecoreChildren]
        IEnumerable<INavigationItem> Children { get; set; }

        //used in code only
        string LinkItemCssClass { get; set; }
    }
}