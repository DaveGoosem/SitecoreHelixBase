using Sample.Foundation.Search.Models;
using Sitecore.ContentSearch;
using System.Runtime.Serialization;

namespace Sample.Foundation.Locations.Models
{
    public class LocationSearchResultItem : BaseSearchResultItem
    {
        [DataMember]
        [IndexField("latitude_t")]
        public string Latitude { get; set; }
        [DataMember]
        [IndexField("longitude_t")]
        public string Longitude { get; set; }
        [DataMember]
        [IndexField("country_sm")]
        public string Country { get; set; }
        [DataMember]
        [IndexField("state_t")]
        public string State { get; set; }
        [DataMember]
        [IndexField("suburb_t")]
        public string Suburb { get; set; }
        [DataMember]
        [IndexField("postcode_t")]
        public string Postcode { get; set; }
    }
}