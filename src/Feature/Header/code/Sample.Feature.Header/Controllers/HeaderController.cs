using Sample.Feature.Header.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Header.Controllers
{
    [LoggingFilter]
    public class HeaderController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;        

        public HeaderController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;            
        }

        public ActionResult Header()
        {
            // Get datasource model data from glass mapper
            IHeader datasourceItem = _glassMapperService.GetDataSourceItem<IHeader>();
            if (datasourceItem != null)
            {
                _logService.DebugInfo(string.Format("Added Header to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/Header/Header.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}