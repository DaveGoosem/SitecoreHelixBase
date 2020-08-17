using System.Collections.Generic;
using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Navigation.Models
{
    [SitecoreType(TemplateId = "{754812BE-7BBF-4C09-B5A9-247611F6C90F}", AutoMap = true)]
    public interface IFooterSecondary : IGlassBase
    {
        string FooterSecondaryCopyrightText { get; set; }
        IEnumerable<INavigationItem> FooterSecondaryPages { get; set; }
        Image FooterSecondaryDecorImage { get; set; }
    }
}