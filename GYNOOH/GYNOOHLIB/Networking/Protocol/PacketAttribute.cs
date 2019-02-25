using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Protocol
{
    public class PacketAttribute : System.Attribute
    {
        public PacketAttribute(string name, Type type)
        {
            Name = name;
            pkType = type;
        }
        public string Name;
        public Type pkType;
    }
}
