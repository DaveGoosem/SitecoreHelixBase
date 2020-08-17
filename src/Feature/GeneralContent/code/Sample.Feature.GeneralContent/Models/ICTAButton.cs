using Sample.Foundation.Forms.Models;
using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{5DAE156F-FEC0-4C49-A0D4-7FF9A45A83BC}", AutoMap = true)]
    public interface ICTAButton : IButton, IGlassBase
    {
        Button CTAButton { get; set; }
    }
}
