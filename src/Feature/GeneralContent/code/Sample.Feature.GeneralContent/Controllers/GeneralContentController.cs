using Sample.Feature.GeneralContent.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.GeneralContent.Controllers
{
    [LoggingFilter]
    public class GeneralContentController : SitecoreController
    {
        readonly IGlassMapperService _glassMapperService;
        readonly ILogService _logger;        

        public GeneralContentController(IGlassMapperService glassMapperService, ILogService logger)
        {
            _glassMapperService = glassMapperService;
            _logger = logger;            
        }

        public ActionResult ContentBlock()
        {
            // Get datasource model data from glass mapper
            IContentBlock datasourceItem = _glassMapperService.GetDataSourceItem<IContentBlock>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo(string.Format("Added General Content Block to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/GeneralContent/ContentBlock.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult AlertBar()
        {
            // Get datasource model data from glass mapper
            IAlertBar datasourceItem = _glassMapperService.GetDataSourceItem<IAlertBar>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo(string.Format("Added Alert Bar to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/GeneralContent/AlertBar.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult CTAButton()
        {
            // Get datasource model data from glass mapper
            ICTAButton datasourceItem = _glassMapperService.GetDataSourceItem<ICTAButton>();
            if (datasourceItem != null)
            { 
                datasourceItem.CTAButton = Foundation.Forms.Extensions.EditorTemplateMapper.MapButton(datasourceItem);
                _logger.DebugInfo(string.Format("Added CTA Button to page with following data source: {0}", datasourceItem.FullPath));

                return PartialView("~/Views/Feature/GeneralContent/CTAButton.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult ResponsiveImage()
        {
            // Get datasource model data from glass mapper
            IResponsiveImage datasourceItem = _glassMapperService.GetDataSourceItem<IResponsiveImage>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo(string.Format("Added Responsive Image to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/GeneralContent/ResponsiveImage.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult QuickLink()
        {
            // Get datasource model data from glass mapper
            IQuickLink datasourceItem = _glassMapperService.GetDataSourceItem<IQuickLink>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo(string.Format("Added Quick link to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/GeneralContent/QuickLink.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult ImageQuickLink()
        {
            // Get datasource model data from glass mapper
            IImageQuickLink datasourceItem = _glassMapperService.GetDataSourceItem<IImageQuickLink>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo(string.Format("Added Image Quick link to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/GeneralContent/ImageQuickLink.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult PageSection()
        {
            // Get datasource model data from glass mapper
            IPageSection datasourceItem = _glassMapperService.GetDataSourceItem<IPageSection>();
            if (datasourceItem != null)
            {
                _logger.DebugInfo($"Added a Page Section to page with following data source: {datasourceItem.FullPath}");
                return PartialView("~/Views/Feature/GeneralContent/PageSection.cshtml", datasourceItem);
            }          

            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}