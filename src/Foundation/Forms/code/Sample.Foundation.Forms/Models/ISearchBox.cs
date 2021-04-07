using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;

namespace Sample.Foundation.Forms.Models
{
    public interface ISearchBox : IGlassBase
    {
        //style: Style of the search box.Allowed values: light | dark
        IStyle SearchBoxStyle { get; set; }        

        // size: Size of the search box. Allowed values: small | medium | large
        ISize SearchBoxSize { get; set; }

        //Placeholder text to show inside the search box.
        string PlaceholderText { get; set; }

        //Whether or not to show an icon inside the search box (defaults to a search icon if `custom-icon` is not specified).
        bool ShowIcon { get; set; }

        //Icon to show inside the search box, in FontAwesome class format (e.g. "fas fa-search").
        string CustomIconClass { get; set; }
    }
}