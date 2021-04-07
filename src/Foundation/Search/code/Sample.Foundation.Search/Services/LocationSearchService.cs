using Sitecore.ContentSearch.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Sitecore.ContentSearch;
using Sample.Foundation.Logging.Services;
using Sample.Foundation.Search.Models;
using Sitecore.ContentSearch.Utilities;

namespace Sample.Foundation.Search.Services
{
    public class LocationSearchService<T> : ILocationSearchService<T> where T : BaseLocationSearchResultItem
    {
        private readonly ILogService _logService;

        public LocationSearchService()
        {
            _logService = DependencyResolver.Current.GetService<ILogService>();
        }

        public Models.SearchResults<T> SearchWithLocation(LocationSearchQuery query, Expression<Func<T, bool>> predicate)
        {
            if (query == null || query.Coordinates == null)
                return null;

            IQueryable<T> searchResults = null;

            try
            {
                foreach (var coordinate in query.Coordinates)
                {
                    //Check we have a coord set
                    if (coordinate != null)
                    {
                        using (var searchContext = CreateSearchContextForIndex(query.SelectedSearchIndex))
                        {
                            if (searchResults == null)
                            {
                                searchResults = searchContext.GetQueryable<T>().Where(predicate)
                                    .WithinRadius(s => s.Location, coordinate, query.SearchRadius)
                                    .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
                            }
                            else
                            {
                                searchResults.Concat(searchContext.GetQueryable<T>().Where(predicate))
                                    .WithinRadius(s => s.Location, coordinate, query.SearchRadius)
                                    .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                //fail silently but log error
                _logService.Error(string.Format("unable to search on index {0}. Error was {1}.", query.SelectedSearchIndex, ex));
            }

            if (query.PaginationEnabled)
            {
                var pagedResults = GetPagedResults(query, searchResults);
                return pagedResults;
            }

            if (searchResults == null)
                return null;

            //If we don't want to use pagination, we will return the full set of results in a single "page"           
            var resultsResponse = new Models.SearchResults<T>(query);
            var results = searchResults.GetResults();
            resultsResponse.Results = results.Hits.ToList();
            resultsResponse.TotalResults = results.TotalSearchResults;
            resultsResponse.TotalPages = 1;
            return resultsResponse;
        }        

        private Models.SearchResults<T> GetPagedResults(Query query, IQueryable<T> searchResults)
        {
            if (searchResults == null)
                return null;

            var pagedResults = searchResults.Page(query.Page, query.PageSize).GetResults();

            var results = new Models.SearchResults<T>(query);
            results.TotalResults = pagedResults.TotalSearchResults;
            results.TotalPages = (pagedResults.TotalSearchResults - 1) / query.PageSize;
            results.Results = pagedResults.Hits.ToList();

            return results;
        }

        /// <summary>
        /// Checks for a search index with the name supplied and returns a context if it can, othewise will default to the Sitecore Web Index context
        /// </summary>
        /// <param name="searchIndexName"></param>        
        private IProviderSearchContext CreateSearchContextForIndex(string searchIndexName)
        {
            var index = !string.IsNullOrEmpty(searchIndexName) ? ContentSearchManager.GetIndex(searchIndexName) : ContentSearchManager.GetIndex(Constants.SitecoreWebIndex);
            return index.CreateSearchContext();
        }
    }
}