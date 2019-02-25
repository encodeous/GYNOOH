using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GYNOOHLIB.Networking.Unmanaged
{
    [Serializable]
    public class UnmanagedPacket
    {
        public UnmanagedPacket()
        {

        }
        public UnmanagedPacket(byte[] data, PacketType packetType)
        {
            Data = data;
            PacketType = packetType;
        }
        public byte[] Data;
        public PacketType PacketType;
    }
}
