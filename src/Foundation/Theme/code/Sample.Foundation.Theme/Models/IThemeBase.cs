using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Foundation.Theme.Models
{
    [SitecoreType(TemplateId = "{3C0C1424-5AE0-4901-B017-A5C557AFDA14}", AutoMap = true)]
    public interface IThemeBase : IGlassBase
    {
        string ThemeValue { get; set; }
    }
}
