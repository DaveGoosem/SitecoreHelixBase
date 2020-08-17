using Sample.Foundation.Forms.Models;
using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Tile.Models
{   
    [SitecoreType(TemplateId = "{75691A43-5119-41A8-B057-AEE5C1FE09D2}", AutoMap = true)]
    public interface IIconTile : IButton, IGlassBase
    {        
        ITileIconStyle IconStyle { get; set; }

        ISize IconSize { get; set; }

        string HeadingText { get; set; }

        string Text { get; set; }

        Link Link { get; set; }

        Button CTAButton { get; set; }
    }
}
