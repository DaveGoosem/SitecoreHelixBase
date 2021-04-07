using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Foundation.Locations.Models
{
    [SitecoreType(TemplateId = "{61DD9945-D88D-4E06-8413-6BE15099B379}", AutoMap = true, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public interface ICountry : IGlassBase
    {
        string Name { get; set; }
        string ISO2Code { get; set; }
        string ISO3Code { get; set; }
    }
}