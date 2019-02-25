using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol.Packets
{
    [Packet("Ping", typeof(Ping))]
    public class Ping : Packet
    {
        public void SendPingRequest()
        {
            Send(this);
        }
        public override void OnPacketReceived(object receivedPacket)
        {
            PingResponse response = new PingResponse();
            Send(response); // respond ping
        }
    }
}
