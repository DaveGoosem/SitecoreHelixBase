using Sample.Feature.Social.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using Sample.Foundation.Forms.Models;

namespace Sample.Feature.Social.Controllers
{

    [LoggingFilter]
    public class SocialController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public SocialController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult SocialFollow()
        {
            // Get datasource model data from glass mapper
            ISocialFollowFolder datasourceItem = _glassMapperService.GetDataSourceItem<ISocialFollowFolder>();
            if (datasourceItem != null)
            {
                //We have a list of sitecore inherited Button items from the foundation forms inherited fields, we need to map these to the Editor Template Model for display
                datasourceItem.SocialFollowButtons = new List<Button>();
                foreach (var followItem in datasourceItem.SocialFollowItems)
                {
                    datasourceItem.SocialFollowButtons.Add(Foundation.Forms.Extensions.EditorTemplateMapper.MapButton(followItem));
                }
                
                _logService.DebugInfo(string.Format("Added Social Follow Item to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/Social/SocialFollow.cshtml", datasourceItem.SocialFollowButtons);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}