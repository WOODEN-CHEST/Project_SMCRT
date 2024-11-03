using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public class PacketEncodeException : PacketException
{
    public PacketEncodeException(string? message) : base(message) { }

    public PacketEncodeException(string? message, Exception? innerException) : base(message, innerException) { }
}