using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public class EntityDefinition
{
    // Fields.
    public NamespacedKey Key { get; private init; }
    public EntityComponent[] StartingComponents { get; private init; }


    // Constructors.
    public EntityDefinition(NamespacedKey key, EntityComponent[] startingComponents)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        StartingComponents = startingComponents?.ToArray() ?? throw new ArgumentNullException(nameof(startingComponents));
    }
}