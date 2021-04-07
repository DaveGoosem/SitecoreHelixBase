using Sample.Foundation.Forms.Models;
using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Search.Models
{
    [SitecoreType(TemplateId = "{A42585A0-9420-4EE6-A770-C7D66A6CD69A}", AutoMap = true)]
    public interface IPhraseSearch : ISearchBox, IGlassBase
    {
        //Non Sitecore Fields
        SearchBox SearchBox { get; set; }
    }
}
