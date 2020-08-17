using Sitecore.Data;

namespace Sample.Feature.DynamicLayout
{
    public class Constants
    {
        public struct Templates
        {
            public struct Row
            {
                public const string TemplateIdGuid = "{06FFB06F-4F74-48EA-99EF-1EF6B595A976}";
                public static readonly ID TemplateID = new ID(TemplateIdGuid);
                public const string TemplateName = "Row";

                public struct Fields
                {
                    public const string RowCssClass = "RowCssClass";
                    public const string RowIsFullWidth = "RowIsFullWidth";
                }
            }

            public struct Column
            {
                public const string TemplateIdGuid = "{F3C26577-DFB4-4778-B985-FB8BEF65C797}";
                public static readonly ID TemplateID = new ID(TemplateIdGuid);
                public const string TemplateName = "Column";

                public struct Fields
                {
                    public const string ColumnLG = "ColumnLG";
                    public const string ColumnMD = "ColumnMD";
                    public const string ColumnSM = "ColumnSM";
                    public const string ColumnXS = "ColumnXS";
                    public const string ColumnCssClass = "ColumnCssClass";
                }
            }
        }
    }
}