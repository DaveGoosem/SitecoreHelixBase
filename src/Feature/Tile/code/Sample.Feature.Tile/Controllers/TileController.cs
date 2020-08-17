using Sample.Feature.Tile.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Tile.Controllers
{
    [LoggingFilter]
    public class TileController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public TileController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult IconTile()
        {
            IIconTile dataSourceItem = _glassMapperService.GetDataSourceItem<IIconTile>();
            if (dataSourceItem != null)
            {
                dataSourceItem.CTAButton = Foundation.Forms.Extensions.EditorTemplateMapper.MapButton(dataSourceItem);

                _logService.DebugInfo($"Added Tile into a page with following data source in : {dataSourceItem.FullPath}");
                return PartialView("~/Views/Feature/Tile/IconTile.cshtml", dataSourceItem);
            }

            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult ImageTile()
        {
            IImageTile dataSourceItem = _glassMapperService.GetDataSourceItem<IImageTile>();
            if (dataSourceItem != null)
            {               
                _logService.DebugInfo($"Added Image Tile into the page with following data source in : {dataSourceItem.FullPath}");
                return PartialView("~/Views/Feature/Tile/ImageTile.cshtml", dataSourceItem);
            }

            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult CalloutTile()
        {
            ICalloutTile dataSourceItem = _glassMapperService.GetDataSourceItem<ICalloutTile>();
            if (dataSourceItem != null)
            {
                _logService.DebugInfo($"Added Callout Tile into the page with following data source in : {dataSourceItem.FullPath}");
                return PartialView("~/Views/Feature/Tile/CalloutTile.cshtml", dataSourceItem);
            }

            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}
