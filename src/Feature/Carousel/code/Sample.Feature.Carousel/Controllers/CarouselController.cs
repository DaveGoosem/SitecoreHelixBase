using Sample.Feature.Carousel.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Carousel.Controllers
{
    [LoggingFilter]
    public class CarouselController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public CarouselController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult GetCarouselSlide()
        {
            ICarousel datasourceItem = _glassMapperService.GetDataSourceItem<ICarousel>();
            return PartialView("~/Views/Feature/Carousel/CarouselSlide.cshtml", datasourceItem);
        }

        public ActionResult GetCarousel()
        {
            ICarousel datasourceItem = _glassMapperService.GetDataSourceItem<ICarousel>();

            if (datasourceItem != null)
            {
                _logService.DebugInfo($"Added a carousel into page with following data source: {datasourceItem.FullPath}");

                return PartialView("~/Views/Feature/Carousel/Carousel.cshtml", datasourceItem);
            }

            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}
