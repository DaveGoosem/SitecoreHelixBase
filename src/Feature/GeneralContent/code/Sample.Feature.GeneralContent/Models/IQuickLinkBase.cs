using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{881AD934-F378-41D5-BDA7-FA7ABFFA56C1}", AutoMap = true)]
    public interface IQuickLinkBase : IGlassBase
    {
        Link QuickLinkTarget { get; set; }
    }
}
