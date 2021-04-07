using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Analytics.Models
{
    [SitecoreType(TemplateId = "{C80230B3-BA6C-4584-B09D-FE7B9861595F}", AutoMap = true)]
    public interface ICommonScripts : IGlassBase
    {
        string HeadScripts { get; set; }
        string BodyScripts { get; set; }
    }
}
