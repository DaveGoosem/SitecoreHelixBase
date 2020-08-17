using Sample.Foundation.Forms.Models;
using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Sample.Feature.Social.Models
{
    [SitecoreType(TemplateId = "{90C4B3D5-F2CB-4483-B9AB-8C95B93500C3}", AutoMap = true)]
    public interface ISocialFollowFolder : IGlassBase
    {
        [SitecoreChildren]
        IEnumerable<ISocialFollowItem> SocialFollowItems { get; set; }

        //Non Sitecore properties
        //Button values from Sitecore Get mapped to these for prentation using the Razor Button Editor Template
        List<Button> SocialFollowButtons {get; set;}
    }
}
