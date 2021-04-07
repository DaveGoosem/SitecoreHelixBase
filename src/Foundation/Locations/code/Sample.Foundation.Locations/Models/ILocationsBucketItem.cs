using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Sample.Foundation.Locations.Models
{
    [SitecoreType(TemplateId = "{8807957E-DAF2-4F4D-AEC2-DA3943E3FA25}", AutoMap = true, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface ILocationsBucketItem : IGlassBase     
    {
        [SitecoreChildren]
        IEnumerable<IGlassBase> Children { get; set; }
    }
}
