using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GYNOOHLIB.Networking.Unmanaged;

namespace GYNOOHLIB.Data.Serialization
{
    public static class Serializer
    {
        public static UnmanagedPacket DeserializePacket(byte[] inputBytes)
        {
            inputBytes = Decompress(inputBytes);
            return DeserializeXml<UnmanagedPacket>(inputBytes);
        }
        public static byte[] SerializePacket(UnmanagedPacket inputUnmanagedPacket)
        {
            byte[] buffer = SerializeXml(inputUnmanagedPacket);
            return Compress(buffer);
        }
        public static byte[] SerializeXml<T>(T inputObject)
        {
            using (MemoryStream Output = new MemoryStream())
            {
                var serializer = new XmlSerializer(inputObject.GetType());
                serializer.Serialize(Output, inputObject);
                return Output.ToArray();
            }
        }
        public static T DeserializeXml<T>(byte[] inputBytes)
        {
            using (MemoryStream Input = new MemoryStream(inputBytes))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(Input);
            }
        }
        public static byte[] Compress(byte[] inputBytes)
        {
            using (MemoryStream InputStream = new MemoryStream(inputBytes))
            {
                using (GZipStream GZIPCompressionStream = new GZipStream(InputStream, CompressionMode.Compress))
                {
                    return InputStream.ToArray();

                }
            }

        }
        public static byte[] Decompress(byte[] inputBytes)
        {
            using (MemoryStream InputStream = new MemoryStream(inputBytes))
            {
                using (GZipStream GZIPCompressionStream = new GZipStream(InputStream, CompressionMode.Decompress))
                {
                    return InputStream.ToArray();
                }
            }

        }
    }
}
