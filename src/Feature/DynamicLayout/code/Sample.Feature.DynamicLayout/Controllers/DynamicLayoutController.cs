using System.Web.Mvc;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Services;
using Sample.Feature.DynamicLayout.Models;
using Sitecore.Mvc.Controllers;

namespace Sample.Feature.DynamicLayout.Controllers
{
    [LoggingFilter]
    public class DynamicLayoutController : SitecoreController
    {
        readonly IGlassMapperService _glassMapperService;
        readonly ILogService _logger;        

        public DynamicLayoutController(IGlassMapperService glassMapperService, ILogService logger)
        {
            _glassMapperService = glassMapperService;
            _logger = logger;           
        }

        /// <summary>
        /// Generate dynamic layout based on configuration specified in the DataSource Item. 
        /// </summary>     
        public ActionResult GenerateDynamicLayout()
        {
            // Get datasource model data from glass mapper
            IRow datasourceItem = _glassMapperService.GetDataSourceItem<IRow>();
            if (datasourceItem != null)
            {                               
                _logger.DebugInfo(string.Format("Added Dynamic Layout Row to page with following data source: {0} ", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/DynamicLayout/DynamicLayout.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}