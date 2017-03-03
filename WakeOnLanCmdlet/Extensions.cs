//-----------------------------------------------------------------------
// <copyright file="Extensions.cs">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;

namespace Poesoft
{
    /// <summary>
    /// Class for extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Splits a string into substrings of the specified length
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="length">The length of the substrings to split the string into</param>
        /// <returns>The string split into substrings of the specified length</returns>
        public static string[] SplitByLength(this string value, int length)
        {
            var splitString = new string[value.Length / length];

            var x = 0;

            for (var i = 0; i < value.Length / length; i++)
            {
                splitString[i] = value.Substring(x, length);
                x += length;
            }

            return splitString;
        }

        /// <summary>
        /// Removes the specified characters from the string
        /// </summary>
        /// <param name="value">The string to remove characters from</param>
        /// <param name="chars">The characters to remove</param>
        /// <returns>A copy of the string with the specified characters removed</returns>
        public static string RemoveCharacters(this string value, IEnumerable<char> chars)
        {
            var s = value;
            foreach (var c in chars)
            {
                s = value.Replace(c.ToString(CultureInfo.InvariantCulture), string.Empty);
            }

            return s;
        }
    }
}
