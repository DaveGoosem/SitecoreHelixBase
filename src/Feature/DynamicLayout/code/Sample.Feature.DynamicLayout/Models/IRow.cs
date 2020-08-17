using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;

namespace Sample.Feature.DynamicLayout.Models
{
    [SitecoreType(TemplateId = Constants.Templates.Row.TemplateIdGuid)]
    public interface IRow : IGlassBase
    {
        [SitecoreField(Constants.Templates.Row.Fields.RowCssClass)]
        string RowCssClass { get; set; }

        [SitecoreField(Constants.Templates.Row.Fields.RowIsFullWidth)]
        bool RowIsFullWidth { get; set; }

        [SitecoreChildren(InferType = true)]
        IEnumerable<IColumn> Children { get; set; }
    }
}
