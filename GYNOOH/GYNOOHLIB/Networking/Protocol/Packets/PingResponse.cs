using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol.Packets
{
    [Packet("PingResponse", typeof(PingResponse))]
    public class PingResponse : Packet
    {
        public override void OnPacketReceived(object receivedPacket)
        {
            // do nothing
        }
    }
}
