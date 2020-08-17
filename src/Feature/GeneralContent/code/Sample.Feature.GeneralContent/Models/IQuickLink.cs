using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{CC34ABCD-4401-4888-B7F5-6FBB856DA57C}", AutoMap = true)]
    public interface IQuickLink : IQuickLinkBase
    {
        string QuickLinkText { get; set; }
    }
}
