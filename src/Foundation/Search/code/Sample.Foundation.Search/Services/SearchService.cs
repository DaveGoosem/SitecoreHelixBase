using Sitecore.ContentSearch.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Sitecore.ContentSearch;
using Sample.Foundation.Logging.Services;
using Sample.Foundation.Search.Models;
using Sitecore.ContentSearch.Data;
using System.Collections.Generic;

namespace Sample.Foundation.Search.Services
{
    public class SearchService<T> : ISearchService<T> where T : BaseSearchResultItem
    {        
        private readonly ILogService _logService;

        public SearchService()
        {            
            _logService = DependencyResolver.Current.GetService<ILogService>();
        }

        /// <summary>
        /// Simple Search method which accepts a search query and a predicate to filter your results from the caller
        /// Example predicate could be 
        /// var predicate = PredicateBuilder.True<SolrContentSearchModel>();
        /// predicate = predicate.Or(x => x.ItemName.Contains(query.SearchTerm.ToLower())).Boost(2f);
        /// </summary>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Models.SearchResults<T> Search(Query query, Expression<Func<T, bool>> predicate)
        {
            if (query == null)
                return null;

            IQueryable<T> searchResults = null;

            try
            {
                using (var searchContext = CreateSearchContextForIndex(query.SelectedSearchIndex))
                {
                    if (searchResults == null)
                        searchResults = searchContext.GetQueryable<T>().Where(predicate);
                    else
                        searchResults.Concat(searchContext.GetQueryable<T>().Where(predicate));
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

            //If we don't want to use pagination, we will return the full set of results in a single "page"
            var resultsResponse = new Models.SearchResults<T>(query);
            var results = searchResults.GetResults();
            resultsResponse.Results = results.Hits.ToList();
            resultsResponse.TotalResults = results.TotalSearchResults;
            resultsResponse.TotalPages = 1;
            return resultsResponse;
        }


        //public Models.SearchResults<T> SearchWithLocation(LocationSearchQuery query, Expression<Func<T, bool>> predicate)
        //{
        //    if (query == null || query.Coordinates == null)
        //        return null;

        //    IQueryable<T> searchResults = null;

        //    try
        //    {
        //        foreach (var coordinate in query.Coordinates)
        //        {
        //            //Check we have a coord set
        //            if(coordinate != null)
        //            {
        //                using (var searchContext = CreateSearchContextForIndex(query.SelectedSearchIndex))
        //                {
        //                    if (searchResults == null)
        //                    {
        //                        searchResults = searchContext.GetQueryable<T>().Where(predicate)
        //                            .WithinRadius(s => s.Location, coordinate, query.SearchRadius)
        //                            .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
        //                    }
        //                    else
        //                    {
        //                        searchResults.Concat(searchContext.GetQueryable<T>().Where(predicate))
        //                            .WithinRadius(s => s.Location, coordinate, query.SearchRadius)
        //                            .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
        //                    }
        //                }
        //            }
        //        }

        //        searchResults = CalculateDistancesFromPoints(searchResults, query);

        //        //using (var searchContext = CreateSearchContextForIndex(query.SelectedSearchIndex))
        //        //{
        //        //    if (searchResults == null)
        //        //        searchResults = searchContext.GetQueryable<T>().Where(predicate)
        //        //            .WithinRadius(s => s.Location, coordinate, radius)
        //        //            .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
        //        //    else
        //        //        searchResults.Concat(searchContext.GetQueryable<T>().Where(predicate))
        //        //            .WithinRadius(s => s.Location, coordinate, radius)
        //        //            .OrderByDistance(s => s.Location, string.Format("{0}, {1}", coordinate.Latitude, coordinate.Longitude));
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //fail silently but log error
        //        _logService.Error(string.Format("unable to search on index {0}. Error was {1}.", query.SelectedSearchIndex, ex));
        //    }

        //    if (query.PaginationEnabled)
        //    {
        //        var pagedResults = GetPagedResults(query, searchResults);
        //        return pagedResults;
        //    }

        //    if (searchResults == null)
        //        return null;

        //    //If we don't want to use pagination, we will return the full set of results in a single "page"           
        //    var resultsResponse = new Models.SearchResults<T>(query);
        //    var results = searchResults.GetResults();
        //    resultsResponse.Results = results.Hits.ToList();
        //    resultsResponse.TotalResults = results.TotalSearchResults;
        //    resultsResponse.TotalPages = 1;
        //    return resultsResponse;
        //}

        //private IQueryable<BaseLocationSearchResultItem> CalculateDistancesFromPoints(IQueryable<BaseLocationSearchResultItem> results, LocationSearchQuery query)
        //{
        //    IQueryable updatedResults = null;

        //    foreach (var result in results)
        //    {
        //        if(result.Location != null)
        //        {
        //            foreach (var coordinate in query.Coordinates)
        //            {
        //                //caculate the distance between the result and the supplied co-ordinate and update the result with the 'shortest' distance value out of all the co-ords
        //                DistanceCalculator.Calculator.Calculate(result.Location.Latitude, result.Location.Longitude, coordinate.Latitude, coordinate.Longitude, 'K');
        //            }

                   
        //        }
        //    }
        //}


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