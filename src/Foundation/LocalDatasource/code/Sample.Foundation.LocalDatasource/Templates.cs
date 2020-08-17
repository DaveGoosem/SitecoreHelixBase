using Sitecore.Data;

namespace Sample.Foundation.LocalDatasource
{
    public class Templates
    {
        public struct RenderingOptions
        {
            //this is the id of the actual rendering options located here: /sitecore/templates/System/Layout/Sections/Rendering Options
            public static ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");

            public struct Fields
            {
                //this is the ID of the custom additional field we've added to the above template to manage enabling/disabling local datasource
                public static readonly ID SupportsLocalDatasource = new ID("{971C3795-FFBA-41E7-8606-F959FE5EED88}");
            }
        }

        public struct Index
        {
            public struct Fields
            {
                public static readonly string LocalDatasourceContent_IndexFieldName = "local_datasource_content";
            }
        }
    }
}