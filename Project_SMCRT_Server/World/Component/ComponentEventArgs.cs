using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class ComponentEventArgs
{
    // Fields.
    public EntityComponent Component { get; private init; }
    public ulong Entity { get; private init; }


    // Constructors.
    public ComponentEventArgs(EntityComponent component, ulong entity)
    {
        Component = component ?? throw new ArgumentNullException(nameof(component));
        Entity = entity;
    }
}