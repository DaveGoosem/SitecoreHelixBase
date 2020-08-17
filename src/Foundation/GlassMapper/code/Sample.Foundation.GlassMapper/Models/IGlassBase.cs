using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace Sample.Foundation.GlassMapper.Models
{
    public interface IGlassBase
    {
        [SitecoreField("__created")]
        DateTime CreatedDate { get; set; }

        [SitecoreId]
        Guid Id { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateName)]
        string TemplateName { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        Guid TemplateId { get; set; }

        [SitecoreField("__Updated")]
        DateTime UpdatedDate { get; set; }

        [SitecoreField("__Display name")]
        string DisplayName { get; set; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        string ItemName { get; set; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        string Language { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        string FullPath { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        string PageUrl { get; set; }

        Dictionary<string, string> DictionaryLabels { get; set; }
    }
}
