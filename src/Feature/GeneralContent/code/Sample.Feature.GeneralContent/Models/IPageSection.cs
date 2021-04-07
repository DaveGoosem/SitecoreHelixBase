using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.GeneralContent.Models
{
    [SitecoreType(TemplateId = "{609031F5-0732-45E4-B146-EE1A08A5DEA9}", AutoMap = true)]
    public interface IPageSectionDecorSides 
    {
        string PageSectionDecorSides { get; set; }
    }

    [SitecoreType(TemplateId = "{0859C065-B7EB-48D0-A86F-6FEE9D3BB4F8}", AutoMap = true)]
    public interface IPageSectionDecorTopEdge
    {
        string PageSectionDecorTopEdge { get; set; }
    }

    [SitecoreType(TemplateId = "{9AFB94EA-6255-4F3A-BD64-2D476E920DD2}", AutoMap = true)]
    public interface IPageSection : IGlassBase
    {
        IStyle Style { get; set; }

        IColor Colour { get; set; }

        IPageSectionDecorSides DecorSides { get; set; }

        IPageSectionDecorTopEdge DecorTopEdge { get; set; }

        ISize PaddingValue { get; set; }

        IStyle LayoutStyle { get; set; }
    }
}

