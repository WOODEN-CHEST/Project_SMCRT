using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public class PacketDecodeException : PacketException
{
    public PacketDecodeException(string? message) : base(message) { }

    public PacketDecodeException(string? message, Exception? innerException) : base(message, innerException) { }
}