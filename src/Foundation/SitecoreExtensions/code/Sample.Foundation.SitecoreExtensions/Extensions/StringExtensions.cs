using System;

namespace Sample.Foundation.SitecoreExtensions.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Trims a string to only return the number of characters requested (to the nearest full word).
        /// If you don't provider a number of characters or the number of characters is longer than the string supplied, it will return the entire original string.
        /// </summary>
        /// <param name="numberOfCharacters"></param>        
        public static string TrimContentWithElipsis(this string content, int numberOfCharacters = 0)
        {
            if (numberOfCharacters == 0 || string.IsNullOrEmpty(content) || content.Length <= numberOfCharacters)
            {
                return content;
            }

            //trim the string to the maximum length
            var trimmedString = content.Substring(0, numberOfCharacters);

            //re-trim if we are in the middle of a word
            trimmedString = trimmedString.Substring(0, Math.Min(trimmedString.Length, trimmedString.LastIndexOf(" ")));

            //append ellipsis
            return trimmedString + " ...";
        }

    }
}