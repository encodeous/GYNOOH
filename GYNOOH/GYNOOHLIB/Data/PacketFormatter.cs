using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYNOOHLIB.Data.Serialization;

namespace GYNOOHLIB.Data
{
    public class PacketFormatter
    {
        public static byte[] Serialize(object p)
        {
            var a = Serializer.SerializeXml(p);
            a = Serializer.Compress(a);
            a = EncryptBytes(a);
            return a;
        }
        public static object Deserialize(byte[] p)
        {
            var a = DecryptBytes(p);
            a = Serializer.Decompress(a);
            var b = Serializer.DeserializeXml<object>(a);
            return b;
        }

        public static byte[] EncryptBytes(byte[] i)
        {
            return i;
        }
        public static byte[] DecryptBytes(byte[] i)
        {
            return i;
        }
    }
}
