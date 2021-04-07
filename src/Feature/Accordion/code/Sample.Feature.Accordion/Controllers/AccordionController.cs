using Sample.Feature.Accordion.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Accordion.Controllers
{
    [LoggingFilter]
    public class AccordionController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public AccordionController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult AccordionItem()
        {
            // Get datasource model data from glass mapper
            IAccordionItem datasourceItem = _glassMapperService.GetDataSourceItem<IAccordionItem>();
            if (datasourceItem != null)
            {
                _logService.DebugInfo(string.Format("Added Accordion Item to an accordion with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/Accordion/AccordionItem.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult Accordion()
        {
            // Get datasource model data from glass mapper
            IAccordionItem datasourceItem = _glassMapperService.GetDataSourceItem<IAccordionItem>();

            if (datasourceItem != null)
            {
                _logService.DebugInfo(string.Format("Added Accordion to a page with following data source: {0}", datasourceItem.FullPath));

                return PartialView("~/Views/Feature/Accordion/Accordion.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult AccordionListing()
        {
            // Get datasource model data from glass mapper
            IAccordionListing datasourceItem = _glassMapperService.GetDataSourceItem<IAccordionListing>();
            if (datasourceItem != null)
            {
                _logService.DebugInfo(string.Format("Added an Accordion Listing into page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/Accordion/AccordionListing.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}