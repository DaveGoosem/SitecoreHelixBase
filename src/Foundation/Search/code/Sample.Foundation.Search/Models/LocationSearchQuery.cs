using Sitecore.ContentSearch.Data;
using System.Collections.Generic;

namespace Sample.Foundation.Search.Models
{
    public class LocationSearchQuery : Query
    {
        //you can supply as many coordinates as you need to search on
        public List<Coordinate> Coordinates { get; set; }
        
        public double SearchRadius { get; set; }
    }
}