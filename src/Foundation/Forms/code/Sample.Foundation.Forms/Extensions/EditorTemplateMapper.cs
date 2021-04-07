using Sample.Foundation.Forms.Models;

namespace Sample.Foundation.Forms.Extensions
{
    public static class EditorTemplateMapper
    {
        public static SearchBox MapSearchBox(this ISearchBox searchBoxType)
        {
            return new SearchBox
            {
                Id = searchBoxType.Id,
                PlaceholderText = searchBoxType.PlaceholderText,
                CustomIconClass = searchBoxType.CustomIconClass,
                SearchBoxSize = searchBoxType.SearchBoxSize?.ThemeValue,
                SearchBoxStyle = searchBoxType.SearchBoxStyle?.ThemeValue,
                ShowIcon = searchBoxType.ShowIcon
            };
        }


        public static Button MapButton(this IButton buttonType)
        {
            return new Button
            {          
                Id = buttonType.Id,
                ButtonIcon = buttonType.ButtonIcon,
                ButtonLink = buttonType.ButtonLink,
                ButtonType = buttonType.ButtonType?.ThemeValue,
                ButtonSize = buttonType.ButtonSize?.ThemeValue,
                ButtonStyle = buttonType.ButtonStyle?.ThemeValue,
                ButtonText = buttonType.ButtonText,
                UseBigIcon = buttonType.UseBigIcon,
                StretchToFillContainer = buttonType.StretchToFillContainer
            };
        }
    }
}