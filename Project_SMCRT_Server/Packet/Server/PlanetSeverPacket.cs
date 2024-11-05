using GHEngine.IO.GHDF;
using Project_SMCRT_Server.Packet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet.Server;

public abstract class PlanetSeverPacket : ServerPacket
{
    // Fields.
    public ulong PlanetID { get; private init; }


    // Constructors.
    protected PlanetSeverPacket(PacketType type, ulong planetID) : base(type)
    {
        PlanetID = planetID;
    }

    // Inherited methods.
    public abstract override GHDFCompound Encode();
}