using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{8888E5DE-2933-4007-BAF5-D710AF2BD7E8}", AutoMap = true)]
    public interface IImageQuickLink : IQuickLinkBase
    {
        Image QuickLinkImage { get; set; }
    }
}
