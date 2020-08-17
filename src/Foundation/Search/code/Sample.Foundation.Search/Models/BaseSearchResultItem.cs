using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Runtime.Serialization;

namespace Sample.Foundation.Search.Models
{
    public class BaseSearchResultItem : SearchResultItem
    {
        [DataMember]
        [IndexField("_indexname")]
        public string IndexName { get; set; }

        [DataMember]
        [IndexField("haschildren_b")]
        public bool HasChildren { get; set; }

        [DataMember]
        public float Score { get; set; }
    }
}