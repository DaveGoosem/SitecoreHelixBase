using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Tile.Models
{
    [SitecoreType(TemplateId = "{B0A0A430-B141-4151-AB02-D5CECF21A1D6}", AutoMap = true)]
    public interface ITileIconStyle : IGlassBase
    {
        string Icon { get; set; }
    }
}
