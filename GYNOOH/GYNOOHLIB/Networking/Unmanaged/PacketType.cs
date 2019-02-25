using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.Networking.Unmanaged
{
    public enum PacketType
    {
        ProtocolCheck, // Initiation: 4 byte array containing an int that is the protocol version
        ProtocolResponse, // Responds with the target protocol version
        PublicKey, // Sends out the public key
        PublicKeyResponse, // Responds with the target's public key
        VerifyKey, // Sends out the Encryption Key hashed with SHA 512
        Success, // Generic success response
        Error, // Generic error response
        ManagedPacket // The message is apart of the protocol
    }
}
