using Sitecore.ContentSearch.Linq;
using System.Collections.Generic;

namespace Sample.Foundation.Search.Models
{
    public class SearchResults<T>
    {
        public SearchResults(Query query)
        {
            Query = query;
            TotalPages = 0;
            TotalResults = 0;
            Results = new List<SearchHit<T>>();
        }

        public Query Query { get; set; }
        //IEnumerable<ISearchResultFacet> Facets { get; }
        public List<SearchHit<T>> Results { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
    }
}