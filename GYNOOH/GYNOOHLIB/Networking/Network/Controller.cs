using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GYNOOHLIB.Data.Serialization;
using GYNOOHLIB.Encryption;
using GYNOOHLIB.IO;
using GYNOOHLIB.Networking.Protocol;
using GYNOOHLIB.Networking.Unmanaged;

namespace GYNOOHLIB.Networking.Network
{
    public class Controller
    {
        public static Client client = new Client();
        public static Server server = new Server();
        public static List<PacketType> PrevPackets = new List<PacketType>();
        public static ECC EllipticCurveCrypto = new ECC();
        public static byte[] sharedKey;
        public static bool wait = false;
        public static PacketType waitType;
        public static bool FullyConnected;
        public static void StartListening()
        {
            server.OnServerMessageEvent += Handler;
            server.Listen();
        }

        public static void Connect(IPAddress address, int port)
        {
            if (!client.client.Connected)
            {
                client.Connect(address, port);
                while (!FullyConnected)
                {

                }
            }
            else
            {
                Logger.Log("Error","Client is already connected to another target.");
            }
        }
        public static void Disconnect()
        {
            if (!client.client.Connected)
            {
                Logger.Log("Info", "Target has already disconnected");
            }
            else
            {
                Logger.Log("Info", "Disconnecting from target...");
                client.client.Close();
                Logger.Log("Info", "Restarting Listener...");
                server.server.Stop();
                server.Listen();
            }
        }

        public static void Handler(ServerMessageEventArgs e)
        {
            var unmanagedPacket = Serializer.DeserializePacket(e.DataBytes);
            if (wait)
            {
                if (unmanagedPacket.PacketType != waitType)
                {
                    return;
                }
                wait = false;
            }
            switch (unmanagedPacket.PacketType)
            {
                case PacketType.ProtocolCheck:
                    if (BitConverter.ToInt32(unmanagedPacket.Data,0) == ProtocolInfo.ProtocolVersion)
                    {
                        Send(new UnmanagedPacket(BitConverter.GetBytes(ProtocolInfo.ProtocolVersion), PacketType.ProtocolResponse));
                    }
                    else
                    {
                        Logger.Log("Error", $"Target has connected with ProtocolVersion "+ BitConverter.ToInt32(unmanagedPacket.Data, 0));
                        Send(new UnmanagedPacket(null, PacketType.Error));
                    }
                    break;
                case PacketType.Error:
                    switch (GetPrevPacket())
                    {
                        case PacketType.ProtocolCheck:
                            // Connected to a different version of the chat program
                            Disconnect();
                            break;
                        case PacketType.PublicKey:
                            // Public key response error
                            EstablishSecureConnection();
                            // retry
                            break;
                    }
                    break;
                case PacketType.PublicKey:
                    Logger.Log("Info", "Received Public Key Request!");
                    sharedKey = EllipticCurveCrypto.DeriveKey(unmanagedPacket.Data);
                    Send(new UnmanagedPacket(EllipticCurveCrypto.publicKey,PacketType.PublicKeyResponse));
                    break;
                case PacketType.PublicKeyResponse:
                    Logger.Log("Info", "Target has responded with their public key! Target Public key: "+BitConverter.ToString(unmanagedPacket.Data).Replace("-", ""));
                    sharedKey = EllipticCurveCrypto.DeriveKey(unmanagedPacket.Data);
                    break;
                case PacketType.ManagedPacket:
                    PacketHandler.HandlePackets(unmanagedPacket);
                    break;
            }
        }

        public static void Send(UnmanagedPacket packet)
        {
            PrevPackets.Add(packet.PacketType);
            client.Send(Serializer.SerializePacket(packet));
        }
        public static void SendWait(UnmanagedPacket packet, PacketType packetType)
        {
            PrevPackets.Add(packet.PacketType);
            client.Send(Serializer.SerializePacket(packet));
            wait = true;
            waitType = packetType;
            while (wait)
            {
                Thread.Sleep(500);
            }
        }
        public static PacketType GetPrevPacket()
        {
            return PrevPackets[PrevPackets.Count - 1];
        }

        public static void ProtocolConnect()
        {
            new Thread(() =>
            {
                Logger.Log("Info", "Checking Protocol Version...");
                SendWait(new UnmanagedPacket(BitConverter.GetBytes(ProtocolInfo.ProtocolVersion), PacketType.ProtocolCheck), PacketType.ProtocolResponse);
                Logger.Log("Info", "Protocol Check Passed!");
                EstablishSecureConnection();
            }).Start();

        }
        public static void EstablishSecureConnection()
        {
            Logger.Log("Info", "Establishing Secure Connection with public key: "+ BitConverter.ToString(EllipticCurveCrypto.publicKey).Replace("-", ""));
            SendWait(new UnmanagedPacket(EllipticCurveCrypto.publicKey, PacketType.PublicKey),PacketType.PublicKeyResponse);
        }
    }
}
