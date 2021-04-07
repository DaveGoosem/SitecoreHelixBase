using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Accordion.Models
{
    [SitecoreType(TemplateId = "{077024C4-4206-4FC6-8751-F2B7417D7270}", AutoMap = true)]
    public interface IAccordionItem : IGlassBase
    {
        string AccordionHeaderText { get; set; }
        string AccordionBodyText { get; set; }        
    }
}
