using System;
using System.Linq;
using System.Web.Mvc;
using Sample.Feature.Navigation.Models;
using Sample.Feature.Navigation.Services;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;

namespace Sample.Feature.Navigation.Controllers
{
    [LoggingFilter]
    public class NavigationController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;
        private readonly INavigationService _navigationService;

        public NavigationController(IGlassMapperService glassMapperService, ILogService logService, INavigationService navigationService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
            _navigationService = navigationService;
        }

        private static string CopyrightTextTransform(string rawCopyrightText)
        {
            var currentYear = DateTime.Today.Year.ToString();
            var transformedCopyrightText = rawCopyrightText.Replace("{copyright}", "&#169;");
            transformedCopyrightText = transformedCopyrightText.Replace("{year}", currentYear);
            return transformedCopyrightText;
        }

        public ActionResult DesktopNavigation()
        {
            var homeItem = _glassMapperService.GetStartItem<INavigationItem>(_glassMapperService.GetMvcContext());
            var desktopNavigationItems = new NavigationItems { NavItems = _navigationService.GetNavigationItems(homeItem, 0, 1) };
            if (desktopNavigationItems != null && desktopNavigationItems.NavItems.Any())
            {
                _logService.DebugInfo($"Added Desktop Navigationto page.");
                return PartialView("~/Views/Feature/Navigation/DesktopNavigation.cshtml", desktopNavigationItems);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult MobileNavigation()
        {
            var homeItem = _glassMapperService.GetStartItem<INavigationItem>(_glassMapperService.GetMvcContext());
            var mobileNavigationItems = new NavigationItems { NavItems = _navigationService.GetNavigationItems(homeItem, 0, 1) };
            if (mobileNavigationItems != null && mobileNavigationItems.NavItems.Any())
            {
                _logService.DebugInfo($"Added Mobile Navigationto page.");
                return PartialView("~/Views/Feature/Navigation/MobileNavigation.cshtml", mobileNavigationItems);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }

        public ActionResult FooterPrimary()
        {
            IFooter dataSourceItem = _glassMapperService.GetDataSourceItem<IFooter>();
            if (dataSourceItem == null)
                return PartialView("~/Views/Common/NoDataSourceItem.cshtml");

            _logService.DebugInfo($"Added Footer Primary to page with following data source: {dataSourceItem.FullPath}");
            
            return PartialView("~/Views/Feature/Navigation/FooterPrimary.cshtml", dataSourceItem);
        }

        public ActionResult FooterSecondary()
        {
            IFooterSecondary dataSourceItem = _glassMapperService.GetDataSourceItem<IFooterSecondary>();
            if (dataSourceItem == null)
                return PartialView("~/Views/Common/NoDataSourceItem.cshtml");

            _logService.DebugInfo($"Added Footer Secondary to page with following data source: {dataSourceItem.FullPath}");

            // Token Replacement for Footer Copyright Text
            dataSourceItem.FooterSecondaryCopyrightText = CopyrightTextTransform(dataSourceItem.FooterSecondaryCopyrightText);
            return PartialView("~/Views/Feature/Navigation/FooterSecondary.cshtml", dataSourceItem);
        }
    }
}