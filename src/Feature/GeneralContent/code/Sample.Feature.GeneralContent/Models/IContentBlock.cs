using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{9FF66F2B-9499-47D9-937F-D6F4DE4F21BC}", AutoMap = true)]
    public interface IContentBlock : IGlassBase
    {
        string ContentBlockRichText { get; set; }
    }
}
