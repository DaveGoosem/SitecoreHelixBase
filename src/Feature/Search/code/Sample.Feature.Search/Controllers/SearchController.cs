using Sample.Feature.Search.Models;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Logging.Attributes;
using Sample.Foundation.Logging.Services;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Sample.Feature.Search.Controllers
{
    [LoggingFilter]
    public class SearchController : SitecoreController
    {
        private readonly IGlassMapperService _glassMapperService;
        private readonly ILogService _logService;

        public SearchController(IGlassMapperService glassMapperService, ILogService logService)
        {
            _glassMapperService = glassMapperService;
            _logService = logService;
        }

        public ActionResult PhraseSearch()
        {
            // Get datasource model data from glass mapper
            IPhraseSearch datasourceItem = _glassMapperService.GetDataSourceItem<IPhraseSearch>();
            if (datasourceItem != null)
            {
                datasourceItem.SearchBox = Foundation.Forms.Extensions.EditorTemplateMapper.MapSearchBox(datasourceItem);
                _logService.DebugInfo(string.Format("Added Phrase Search to page with following data source: {0}", datasourceItem.FullPath));
                return PartialView("~/Views/Feature/Search/PhraseSearch.cshtml", datasourceItem);
            }
            return PartialView("~/Views/Common/NoDataSourceItem.cshtml");
        }
    }
}