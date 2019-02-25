using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GYNOOHLIB.Data;
using GYNOOHLIB.IO;
using GYNOOHLIB.Networking.Network;
using GYNOOHLIB.Networking.Protocol;
using GYNOOHLIB.Networking.Protocol.Packets;
using GYNOOHLIB.Networking.Unmanaged;

namespace GYNOOHLIB.Networking
{
    public class PacketHandler
    {
        public static List<Packet> registerPackets = new List<Packet>();

        public static void InitializeHandler()
        {
            registerPackets.Add(new InitiationRequest());
        }
        public static void HandlePackets(UnmanagedPacket p)
        {
            Logger.Log("Info","Packet received. Type: "+p.PacketType);
            if (p.PacketType == PacketType.ManagedPacket)
            {
                var o = (ManagedPacket) PacketFormatter.Deserialize(p.Data); // deserialize packet
                MethodInfo method = o.attr.pkType.GetMethod("OnPacketReceived"); // get packet handler and Packet received method
                object classInstance = Activator.CreateInstance(o.attr.pkType, null); // create a new packet handler of type
                object[] parametersArray = { o.Packet}; // generate parameters
                if (method != null) method.Invoke(classInstance, parametersArray); // call packet received method
            }
        }

        public static void SendPacket(object o, object sender)
        {
            var pk = new ManagedPacket
            {
                Packet = o,
                attr = sender.GetType().GetCustomAttribute<PacketAttribute>()
            };
            Controller.Send(new UnmanagedPacket(PacketFormatter.Serialize(pk),PacketType.ManagedPacket));
        }
    }
}
