using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{2A572BA1-645D-4F0B-8C43-84AFFAED91D0}", AutoMap = true)]
    public interface IResponsiveImage : IGlassBase
    {
        Image DesktopImage { get; set; }
        Image MobileImage { get; set; }
        bool ShouldFillParent { get; set; }
        bool KeepMinHeight { get; set; }
    }
}
