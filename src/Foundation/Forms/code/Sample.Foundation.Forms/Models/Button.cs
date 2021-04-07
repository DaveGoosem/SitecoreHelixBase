using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace Sample.Foundation.Forms.Models
{
    public class Button
    {
        [SitecoreId]
        public Guid Id { get; set; }

        public string ButtonStyle { get; set; }

        public string ButtonType { get; set; }

        public string ButtonSize { get; set; }

        public string ButtonText { get; set; }

        public Link ButtonLink { get; set; }

        public string ButtonIcon { get; set; }

        public bool UseBigIcon { get; set; }

        public bool StretchToFillContainer { get; set; }
    }
}