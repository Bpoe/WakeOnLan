namespace Poesoft.WakeOnLanCmdletTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const byte fillValue = 1;
            var array = new byte[100];

            array.SetValues(fillValue, 10, 10);

            Assert.AreEqual(fillValue, array[10]);
            Assert.AreEqual(fillValue, array[19]);
            Assert.AreNotEqual(fillValue, array[9]);
            Assert.AreNotEqual(fillValue, array[20]);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mac = MacAddress.Parse("64-51-06-5D-26-EF");
            var packet1 = CreateMagicPacket(mac.GetAddressBytes());
            var packet2 = CreateMagicPacket2(mac.GetAddressBytes());

            CollectionAssert.AreEquivalent(packet1, packet2);
        }

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

        private static byte[] CreateMagicPacket2(byte[] macAddress)
        {
            // From http://en.wikipedia.org/wiki/Wake-on-LAN
            // The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all
            // 255 (FF FF FF FF FF FF in hexadecimal), followed by sixteen repetitions of the target computer's
            // 48-bit MAC address, for a total of 102 bytes.
            var packet = new byte[102];
            packet.SetValues((byte)255, 0, 6);

            var packetIndex = 6;

            for (var r = 0; r < 16; r++)
            {
                macAddress.CopyTo(packet, packetIndex);
                packetIndex += macAddress.Length;
            }

            return packet;
        }
    }
}
