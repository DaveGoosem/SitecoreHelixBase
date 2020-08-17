using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Header.Models
{
    [SitecoreType(TemplateId = "{D6342F16-DE1C-4D97-B2B4-7879C791640E}", AutoMap = true)]
    public interface IHeader : IGlassBase
    {
        Image HeaderLogoImage { get; set; }
        Link HeaderLogoImageLink { get; set; }
        bool IncludeAlertBarSpace { get; set; }
    }
}
