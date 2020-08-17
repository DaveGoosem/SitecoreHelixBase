using Glass.Mapper.Sc.Configuration.Attributes;
using System;

namespace Sample.Foundation.Forms.Models
{
    public class SearchBox
    {
        [SitecoreId]
        public Guid Id { get; set; }

        public string SearchBoxStyle { get; set; }

        // size: Size of the search box. Allowed values: small | medium | large
        public string SearchBoxSize { get; set; }

        //Placeholder text to show inside the search box.
        public string PlaceholderText { get; set; }

        //Whether or not to show an icon inside the search box (defaults to a search icon if `custom-icon` is not specified).
        public bool ShowIcon { get; set; }

        //Icon to show inside the search box, in FontAwesome class format (e.g. "fas fa-search").
        public string CustomIconClass { get; set; }
    }
}