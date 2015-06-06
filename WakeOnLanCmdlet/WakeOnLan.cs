//-----------------------------------------------------------------------
// <copyright file="WakeOnLan.cs">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------

namespace Poesoft
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Implements Wake On LAN magic packet sending functionality
    /// </summary>
    public class WakeOnLan
    {
        /// <summary>
        /// Sends a Wake On LAN magic packet
        /// </summary>
        /// <param name="macAddress">The MAC address of the machine to wake</param>
        public static void Send(MacAddress macAddress)
        {
            Send(macAddress, IPAddress.Broadcast);
        }

        /// <summary>
        /// Sends a Wake On LAN magic packet
        /// </summary>
        /// <param name="macAddress">The MAC address of the machine to wake</param>
        /// <param name="ipAddress">The IP address to address the magic packet to. This should be the broadcast address for the machine to wake's subnet.</param>
        public static void Send(MacAddress macAddress, IPAddress ipAddress)
        {
            Ensure.ArgumentIsNotNull(macAddress, "macAddress");
            Ensure.ArgumentIsNotNull(ipAddress, "ipAddress");

            var packet = CreateMagicPacket(macAddress.GetAddressBytes());
            SendUdpPacket(ipAddress, 7, packet);
        }

        /// <summary>
        /// Sends a UDP packet
        /// </summary>
        /// <param name="address">The IP address to send the UDP packet to</param>
        /// <param name="port">The port to send the UDP packet to</param>
        /// <param name="data">The data to send in the UDP packet</param>
        private static void SendUdpPacket(IPAddress address, int port, byte[] data)
        {
            var udp = new UdpClient();
            udp.Connect(address, port);
            udp.Send(data, data.Length);
        }

        /// <summary>
        /// Creates the magic packet payload the specified MAC address
        /// </summary>
        /// <param name="macAddress">The MAC address of the computer to create the magic packet for</param>
        /// <returns>The magic packet payload</returns>
        private static byte[] CreateMagicPacket(byte[] macAddress)
        {
            // From http://en.wikipedia.org/wiki/Wake-on-LAN
            // The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all
            // 255 (FF FF FF FF FF FF in hexadecimal), followed by sixteen repetitions of the target computer's
            // 48-bit MAC address, for a total of 102 bytes.
            var packet = new byte[102];

            var packetIndex = 0;

            for (; packetIndex < 6; packetIndex++)
            {
                packet[packetIndex] = 0xff;
            }

            for (var r = 0; r < 16; r++)
            {
                macAddress.CopyTo(packet, packetIndex);
                packetIndex += macAddress.Length;
            }

            return packet;
        }
    }
}
