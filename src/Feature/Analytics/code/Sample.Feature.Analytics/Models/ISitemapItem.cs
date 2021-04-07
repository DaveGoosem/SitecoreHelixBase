using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Feature.Analytics.Models
{
    [SitecoreType(TemplateId = "{3FA84433-7BED-4A6A-8325-818542CADD21}", AutoMap = true)]
    public interface ISiteMapItem : IGlassBase
    {
        bool IncludeInSitemap { get; set; }
        
        [SitecoreChildren]
        IEnumerable<ISiteMapItem> Children { get; set; }

    }
}
