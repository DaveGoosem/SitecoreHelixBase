using Sample.Foundation.GlassMapper.Models;
using Sample.Foundation.Theme.Models;
using Glass.Mapper.Sc.Fields;

namespace Sample.Foundation.Forms.Models
{
    public interface IButton : IGlassBase
    {
        IStyle ButtonStyle { get; set; }

        IStyle ButtonType { get; set; }

        ISize ButtonSize { get; set; }

        string ButtonText { get; set; }

        Link ButtonLink { get; set; }

        string ButtonIcon { get; set; }

        bool UseBigIcon { get; set; }

        bool StretchToFillContainer { get; set; }
    }    
}