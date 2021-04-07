using System.Collections.Generic;

namespace Sample.Foundation.Search.Models
{
    public class Query
    {
        public string QueryText { get; set; }
        public int NoOfResults { get; set; } = 10;
        public Dictionary<string, string[]> Facets { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
        public bool PaginationEnabled { get; set; } = true;
        public string SelectedSearchIndex { get; set; }        
    }
}