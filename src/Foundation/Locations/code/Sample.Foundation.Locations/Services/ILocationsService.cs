using Sample.Foundation.Locations.Models;
using Sample.Foundation.Search.Models;
using System.Collections.Generic;

namespace Sample.Foundation.Locations.Services
{
    public interface ILocationsService
    {
        List<LocationSearchResultItem> SearchLocationsByPostcodeOrSuburb(Query query);
    }
}
