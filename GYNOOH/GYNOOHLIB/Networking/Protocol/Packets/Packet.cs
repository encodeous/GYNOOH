using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol.Packets
{
    public class Packet
    {
        public static void Send(object packet)
        {
            PacketHandler.SendPacket(packet, packet);
        }

        public virtual void OnPacketReceived(Object receivedPacket)
        {

        }
    }
}
