using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public abstract class EntityComponent
{
    // Fields.
    public NamespacedKey Key { get; private init; }


    // Constructors.
    public EntityComponent(NamespacedKey key)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
    }


    // Methods.
    public abstract EntityComponent CreateCopy();
}