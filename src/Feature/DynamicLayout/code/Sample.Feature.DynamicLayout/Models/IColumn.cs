using Sample.Foundation.GlassMapper.Models;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sample.Feature.DynamicLayout.Models
{
    [SitecoreType(TemplateId = Constants.Templates.Column.TemplateIdGuid)]
    public interface IColumn : IGlassBase
    {
        [SitecoreField(Constants.Templates.Column.Fields.ColumnLG)]
        string ColumnLG { get; set; }

        [SitecoreField(Constants.Templates.Column.Fields.ColumnMD)]
        string ColumnMD { get; set; }

        [SitecoreField(Constants.Templates.Column.Fields.ColumnSM)]
        string ColumnSM { get; set; }

        [SitecoreField(Constants.Templates.Column.Fields.ColumnXS)]
        string ColumnXS { get; set; }

        [SitecoreField(Constants.Templates.Column.Fields.ColumnCssClass)]
        string ColumnCssClass { get; set; }
    }
}
