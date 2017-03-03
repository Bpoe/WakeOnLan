//-----------------------------------------------------------------------
// <copyright file="MacAddress.cs">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace Poesoft
{
    /// <summary>
    /// Provides a MAC address
    /// </summary>
    public class MacAddress
    {
        /// <summary>
        /// A list of possible characters that separator hex values in a MAC address
        /// </summary>
        private static readonly char[] Separators = { ':', '-' };

        /// <summary>
        /// The byte array value of the MAC address
        /// </summary>
        private readonly byte[] _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="MacAddress" /> class.
        /// </summary>
        /// <param name="address">A string that represents the MAC address.</param>
        public MacAddress(byte[] address)
        {
            Ensure.ArgumentIsNotNull(address, "address");

            if (address.Length != 6)
            {
                throw new ArgumentException("address contains a bad MAC address");
            }

            this._value = address;
        }

        /// <summary>
        /// Converts a MAC address string to a <see cref="MacAddress" /> instance.
        /// </summary>
        /// <param name="macString">A string that contains the MAC address.</param>
        /// <returns>A <class cref="MacAddress">MacAddress</class> instance.</returns>
        public static MacAddress Parse(string macString)
        {
            Ensure.ArgumentIsNotNull(macString, "macString");

            if (!IsValid(macString))
            {
                throw new FormatException("The MAC address is not in the right format");
            }

            return new MacAddress(InternalParse(macString));
        }

        /// <summary>
        /// Determines whether a string is a valid MAC address
        /// </summary>
        /// <param name="macString">The string to validate</param>
        /// <param name="macAddress">A <see cref="MacAddress" /> instance of the string</param>
        /// <returns>true if macString is a valid MAC address; otherwise, false.</returns>
        public static bool TryParse(string macString, out MacAddress macAddress)
        {
            try
            {
                macAddress = Parse(macString);
                return true;
            }
            catch
            {
                macAddress = null;
                return false;
            }
        }

        /// <summary>
        /// Converts a MAC address to a string representation
        /// </summary>
        /// <returns>A string that contains the MAC address in hexadecimal format, with separated by a "-".</returns>
        public override string ToString()
        {
            return BitConverter.ToString(this._value);
        }

        /// <summary>
        /// Provides a copy of the MAC address as a byte array
        /// </summary>
        /// <returns>A <see cref="byte" /> array.</returns>
        public byte[] GetAddressBytes()
        {
            return this._value;
        }

        /// <summary>
        /// Parses a string into a byte array
        /// </summary>
        /// <param name="macString">The string to parse</param>
        /// <returns>A <see cref="byte" /> array of the MAC address</returns>
        private static byte[] InternalParse(string macString)
        {
            var hexString = macString.RemoveCharacters(Separators);

            var hexValues = hexString.SplitByLength(2);
            var byteValues = new byte[hexValues.Length];

            for (var i = 0; i < hexValues.Length; i++)
            {
                byteValues[i] = Convert.ToByte(hexValues[i], 16);
            }

            return byteValues;
        }

        /// <summary>
        /// Determines if a string is a valid MAC address
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <returns>True if is it a valid MAC address; otherwise false.</returns>
        private static bool IsValid(string value)
        {
            var macPattern = new Regex("^([A-Fa-f0-9]{2}[:-]?){6}$");
            return macPattern.IsMatch(value);
        }
    }
}
