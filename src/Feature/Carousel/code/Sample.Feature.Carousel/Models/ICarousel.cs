using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Carousel.Models
{
    [SitecoreType(TemplateId = "{05B038AF-FE53-46F8-90DF-4B8C9D42BDDC}", AutoMap = true)]
    public interface ICarousel : IGlassBase
    {
        IColor ButtonColor {get;set;}

        bool Infinite { get; set; }

        bool AdaptiveHeight { get; set; }

        bool VariableWidth { get; set; }

        bool AutoPlay { get; set; }

        int AutoPlaySpeed { get; set; }

        int Speed { set; get; }

        int SlidesToShow { get; set; }

        int SlidesToScroll { get; set; }

        bool CenterMode { get; set; }

        ISize PaddingValue { get; set; }
    }
}
