using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Network
{
    public class Client
    {
        public TcpClient client = new TcpClient();

        public void Connect(IPAddress address, int port)
        {
            client.Connect(address, port);
        }

        public void Send(byte[] dataBytes)
        {
            var stream = client.GetStream();
            stream.Write(new byte []{100,11,15,193,55,92,3,23},0,8);
            stream.Write(BitConverter.GetBytes(dataBytes.Length),0,4);
            stream.Write(dataBytes,0,dataBytes.Length);
        }
    }
}
