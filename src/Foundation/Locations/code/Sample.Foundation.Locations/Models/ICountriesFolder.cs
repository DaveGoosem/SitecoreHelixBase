using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Sample.Foundation.Locations.Models
{
    [SitecoreType(TemplateId = "{A583A529-8549-4DE7-BBBB-507E7F638469}", AutoMap = true, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface ICountriesFolder : IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<ICountry> Countries { get; set; }
    }
}
