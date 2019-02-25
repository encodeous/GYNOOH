using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol.Packets
{
    [Packet("Message", typeof(Message))]
    public class Message : Packet
    {
        public Message(string msg)
        {
            messageText = msg;
        }
        public string messageText;
        public void SendMessage()
        {
            Send(this);
        }
        public override void OnPacketReceived(object receivedPacket)
        {
            
        }
    }
}
