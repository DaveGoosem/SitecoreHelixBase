using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;

namespace Sample.Feature.Navigation.Models
{
    [SitecoreType(TemplateId = "{1790CF89-8222-4AE9-81C8-9ACDB34375E2}", AutoMap = true)]
    public interface IFooter : IGlassBase
    {
        Image FooterLogoImage { get; set; }
        Link FooterLogoLink { get; set; }
        IEnumerable<INavigationItem> FooterPrimaryColumnItems { get; set; }
        IEnumerable<INavigationItem> FooterSecondaryColumnItems { get; set; }
    }
}
