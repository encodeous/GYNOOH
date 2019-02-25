using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GYNOOHLIB.IO;
using GYNOOHLIB.Networking.Protocol;

namespace GYNOOHLIB.Networking.Network
{
    public class Server
    {
        public TcpListener server = new TcpListener(IPAddress.Parse("0.0.0.0"),ProtocolInfo.Port);

        public delegate void ServerMessageDelegate(ServerMessageEventArgs data);

        public ServerMessageDelegate OnServerMessageEvent;

        public void Listen()
        {
            new Thread(() =>
            {
                Logger.Log("Info","Starting Tcp Server...");
                server.Start();
                Logger.Log("Info", "Waiting for connections...");
                var client = server.AcceptTcpClient();
                Logger.Log("Info", "Client Connected from "+client.Client.RemoteEndPoint);
                Logger.Log("Info", "Connecting back to target for 2 way communication");
                Controller.Connect(((IPEndPoint)client.Client.RemoteEndPoint).Address, ProtocolInfo.Port);
                Controller.FullyConnected = true;
                while (client.Connected)
                {
                    MemoryStream ms = new MemoryStream();
                    var stream = client.GetStream();
                    long readLen = -1;
                    long byteLength = -1;

                    byte[] buf = new byte[8];
                    while (buf != new byte[] { 100, 11, 15, 193, 55, 92, 3, 23 })
                    {
                        buf[8] = buf[7];
                        buf[7] = buf[6];
                        buf[6] = buf[5];
                        buf[5] = buf[4];
                        buf[4] = buf[3];
                        buf[3] = buf[2];
                        buf[2] = buf[1];
                        buf[1] = buf[0];
                        buf[0] = (byte)stream.ReadByte();
                    }
                    byte[] buffer = new byte[4];
                    stream.Read(buffer, 8, 4);
                    byteLength = BitConverter.ToInt32(buffer, 0);
                    while (readLen+64 <= byteLength)
                    {
                        var bf = new byte[64];
                        stream.Read(bf, 0, 64);
                        ms.Write(bf,0,64);
                    }
                    if (readLen<byteLength)
                    {
                        long a = readLen - byteLength;
                        var bf = new byte[a];
                        stream.Read(bf, 0, (int)a);
                        ms.Write(bf, 0, (int)a);
                    }
                    OnServerMessageEvent.Invoke(new ServerMessageEventArgs(ms.ToArray()));
                }
            }).Start();
        }
    }
    public class ServerMessageEventArgs
    {
        public ServerMessageEventArgs(byte[] data)
        {
            DataBytes = data;
        }
        public byte[] DataBytes;
    }
}
