using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace Sample.Feature.Analytics.Models
{
    [SitecoreType(TemplateId = "{42F2FD49-D70C-4358-A4D5-8279D447FD15}", AutoMap = true)]
    public interface IPageMetaData : IGlassBase
    {
        //Standard meta tags
        string BrowserTitle { get; set; }
        string MetaTitle { get; set; }
        string MetaDescription { get; set; }
        string MetaKeywords { get; set; }
        string MetaDataRobots { get; set; }
        Link CanonicalUrl { get; set; }

        //Open Graph
        string MetaOpenGraphTitle { get; set; }
        string MetaOpenGraphType { get; set; }
        string MetaOpenGraphDescription { get; set; }
        Image MetaOpenGraphImage { get; set; }        

        //Twitter
        string MetaTwitterTitle { get; set; }        
        string MetaTwitterDescription { get; set; }
        Image MetaTwitterImage { get; set; }
        string MetaTwitterCard { get; set; }
    }
}
