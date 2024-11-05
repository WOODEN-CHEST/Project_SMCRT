using GHEngine;
using GHEngine.IO.GHDF;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet.Client;

public class UserInputPacket : ClientPacket
{
    // Fields.
    public ulong ReferenceEntity { get; private init; }
    public DVector2 MouseOffset { get; private init; }
    public IEnumerable<InputAction> InputActions { get; private init; }


    // Constructors.
    public UserInputPacket(ulong referenceEntity, DVector2 mouseOffset, InputAction[]? actions) : base(PacketType.UserInput)
    {
        ReferenceEntity = referenceEntity;
        MouseOffset = mouseOffset;
        actions = actions?.ToArray() ?? Array.Empty<InputAction>();
    }



    // Inherited methods.
    public override GHDFCompound Encode()
    {
        throw new NotImplementedException();
    }
}