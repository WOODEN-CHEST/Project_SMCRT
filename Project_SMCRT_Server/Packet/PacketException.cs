using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public class PacketException : Exception
{
    public PacketException(string? message) : base(message) { }

    public PacketException(string? message, Exception? innerException) : base(message, innerException) { }
}