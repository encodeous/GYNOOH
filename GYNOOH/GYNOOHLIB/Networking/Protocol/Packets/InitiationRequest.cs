using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol.Packets
{
    [Packet("InitiationRequest",typeof(InitiationRequest))]
    public class InitiationRequest : Packet
    {
        public override void OnPacketReceived(object receivedPacket)
        {
            // send back packet
        }
    }
}
