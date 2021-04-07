using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace Sample.Foundation.Locations.Models
{    
    [SitecoreType(TemplateId = Constants.LocationsItemTemplateGuid, AutoMap = true, EnforceTemplate = SitecoreEnforceTemplate.Template)]
    public class Location 
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }
        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string ItemName { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual Guid Country { get; set; }
        public virtual string State { get; set; }
        public virtual string Suburb { get; set; }
        public virtual string Postcode { get; set; }
    }
}