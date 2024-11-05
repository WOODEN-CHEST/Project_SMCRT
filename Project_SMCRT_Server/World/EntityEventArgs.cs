using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class EntityEventArgs : EventArgs
{
    // Fields.
    public ulong Entity { get; private init; }


    // Constructors.
    public EntityEventArgs(ulong entity)
    {
        Entity = entity;
    }
}