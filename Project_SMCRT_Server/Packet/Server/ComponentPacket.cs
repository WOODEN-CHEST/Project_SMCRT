using GHEngine.IO.GHDF;
using Project_SMCRT_Server.Packet.Client;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet.Server;

public class ComponentPacket : ServerPacket
{
    // Fields.
    public ulong Entity { get; private init; }
    public EntityComponent Component { get; private init; }


    // Constructors,
    public ComponentPacket(ComponentPacketType type, ulong entityID, EntityComponent component) : base(PacketType.ComponentPacket)
    {
        Entity = entityID;
        Component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public ComponentPacket(GHDFCompound encodedPacket) : base(PacketType.ComponentPacket)
    {

    }


    // Inherited methods.
    public override GHDFCompound Encode()
    {
        throw new NotImplementedException();
    }
}