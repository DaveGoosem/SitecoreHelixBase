using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Foundation.LocalDatasource
{
    public static class Settings
    {
        private static readonly string LocalDatasourceFolderNameSetting = "Sample.Foundation.LocalDatasource.LocalDatasourceFolderName";
        private static readonly string LocalDatasourceFolderNameDefault = "LocalContent";
        private static readonly string LocalDatasourceFolderTemplateSetting = "Sample.Foundation.LocalDatasource.LocalDatasourceFolderTemplate";

        public static string LocalDatasourceFolderName => Sitecore.Configuration.Settings.GetSetting(LocalDatasourceFolderNameSetting, LocalDatasourceFolderNameDefault);
        public static string LocalDatasourceFolderTemplate => Sitecore.Configuration.Settings.GetSetting(LocalDatasourceFolderTemplateSetting);
    }
}