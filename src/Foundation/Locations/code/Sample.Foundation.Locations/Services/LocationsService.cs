using Sample.Foundation.DependencyInjection;
using Sample.Foundation.GlassMapper.Services;
using Sample.Foundation.Locations.Models;
using Sample.Foundation.Logging.Services;
using Sample.Foundation.Search.Models;
using Sample.Foundation.Search.Services;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Foundation.Locations.Services
{
    [Service(typeof(ILocationsService))]
    public class LocationsService : ILocationsService
    {        
        private ILogService _logService;
        private IGlassMapperService _glassMapperService;
        public LocationsService(ILogService logService, IGlassMapperService glassMapperService)
        {
            _logService = logService;
            _glassMapperService = glassMapperService;
        }

        /// <summary>
        /// Search method to query Locations in the Custom Locations Search Index using postcode as the QueryText        
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A list of Location Search Result items</returns>
        public List<LocationSearchResultItem> SearchLocationsByPostcodeOrSuburb(Query query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryText))
                return null;

            if(string.IsNullOrEmpty(query.SelectedSearchIndex))
                query.SelectedSearchIndex = "g8_locations_index_web";            

            var centresBucketItem = _glassMapperService.GetItem<Item>(Guid.Parse(Constants.LocationsItemBucketGuid));
            var searchService = new SearchService<LocationSearchResultItem>();
            var predicate = PredicateBuilder.True<LocationSearchResultItem>();

            //filter out the results to JUST be of the right template we want (IE no bucket folder items)     
            ID locationsItemTemplateID = new ID(Guid.Parse(Constants.LocationsItemTemplateGuid));
            predicate = predicate.And(x => x.TemplateId == locationsItemTemplateID);

            //lets build a sub-predicate to handle postcode OR Suburb
            var postcodeOrSuburbPredicate = PredicateBuilder.True<LocationSearchResultItem>();
            postcodeOrSuburbPredicate = postcodeOrSuburbPredicate.Or(x => x.Postcode.StartsWith(query.QueryText));
            postcodeOrSuburbPredicate = postcodeOrSuburbPredicate.Or(x => x.Suburb.Contains(query.QueryText));

            //lets plug the base predicate back with an AND so we should have "must be X template AND either a Postcode OR a Suburb" as a predicate logic tree
            predicate = predicate.And(postcodeOrSuburbPredicate);

            var searchResults = searchService.Search(query, predicate);
            if (searchResults != null)
            {
                return searchResults.Results.Select(x => x.Document).ToList();
            }

            return null;
        }

    }
}