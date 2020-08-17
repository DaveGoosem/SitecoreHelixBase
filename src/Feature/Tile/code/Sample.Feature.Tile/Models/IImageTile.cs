using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Tile.Models
{
    [SitecoreType(TemplateId = "{B01AFA76-33F5-4D3B-8BC8-634D152E28C9}", AutoMap = true)]
    public interface IImageTile : IGlassBase
    {
        Image Image { get; set; }
        
        string HeadingText { get; set; }        

        Link TileLink { get; set; }        
    }
}
