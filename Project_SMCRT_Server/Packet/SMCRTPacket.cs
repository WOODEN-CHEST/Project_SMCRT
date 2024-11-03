using GHEngine.IO.GHDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public abstract class SMCRTPacket
{
    // Fields.
    public PacketType Type { get; private init; }
    public PacketSource Source { get; private init; }



    // Constructors.
    public SMCRTPacket(PacketType type, PacketSource source)
    {
        Type = type;
        Source = source;
    }


    // Methods.
    public GHDFCompound Encode()
    {

    }
}