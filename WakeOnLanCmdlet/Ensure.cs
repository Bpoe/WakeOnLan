//-----------------------------------------------------------------------
// <copyright file="Ensure.cs">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Poesoft
{
    /// <summary>
    /// Ensures certain conditions are met.
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Throws an ArgumentNullException if the value is null
        /// </summary>
        /// <param name="value">The value to test for null</param>
        /// <param name="name">The name of the argument to display in the exception</param>
        public static void ArgumentIsNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
