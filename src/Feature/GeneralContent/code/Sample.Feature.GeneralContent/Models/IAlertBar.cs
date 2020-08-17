using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{3CF845D1-A4C7-4D7A-A332-88302C286B68}", AutoMap = true)]
    public interface IAlertBar : IGlassBase
    {
        Link AlertBarLink { get; set; }
        string AlertBarText { get; set; }
    }
}
