using Sample.Feature.Analytics.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Analytics.Controllers
{
    [LoggingFilter]
    public class MetaDataController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public MetaDataController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult PageMetaData()
        {
            // Get datasource model data from glass mapper
            IPageMetaData datasourceItem = _glassMapperService.GetCurrentItem<IPageMetaData>();
            _logService.DebugInfo(string.Format("Added Page Meta Data to page."));
            return PartialView("~/Views/Feature/Analytics/PageMetaData.cshtml", datasourceItem);
        }

    }
}