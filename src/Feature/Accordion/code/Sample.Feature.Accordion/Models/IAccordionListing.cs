using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Sample.Feature.Accordion.Models
{
    [SitecoreType(TemplateId = "{2C69A2C8-08C5-4466-9082-234D486D7877}", AutoMap = true)]
    public interface IAccordionListing : IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IAccordionItem> AccordionItems { get; set; }
    }
}
