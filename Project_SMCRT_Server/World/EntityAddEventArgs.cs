using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class EntityAddEventArgs : EntityEventArgs
{
    // Fields.
    public EntityComponent[] Components { get; private init; }


    // Constructors.
    public EntityAddEventArgs(ulong entity, EntityComponent[] components) : base(entity)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
    }
}