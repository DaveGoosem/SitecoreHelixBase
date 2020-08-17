using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Tile.Models
{
    [SitecoreType(TemplateId = "{5D9858D8-4CE5-441D-BCB3-3B8619F91412}", AutoMap = true)]
    public interface ICalloutTile : IGlassBase
    {
        IStyle CalloutStyle { get; set; }

        IColor CalloutColor { get; set; }

        string CalloutText { get; set; }

        string CalloutTitle { get; set; }

        int? CalloutRating { get; set; }
    }
}
