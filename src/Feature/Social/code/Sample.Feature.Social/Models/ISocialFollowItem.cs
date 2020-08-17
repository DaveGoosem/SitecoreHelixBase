using Sample.Foundation.Forms.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.Social.Models
{
    [SitecoreType(TemplateId = "{AC8598DE-026A-4D23-9D86-C91D1C2E8DC7}", EnforceTemplate = Glass.Mapper.Sc.Configuration.SitecoreEnforceTemplate.TemplateAndBase, AutoMap = true)]
    public interface ISocialFollowItem : IButton
    {        
    }
}
