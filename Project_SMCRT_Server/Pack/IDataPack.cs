using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public interface IDataPack
{
    // Fields.
    string Name { get; }
    string Description { get; }
    Version TargetedGameVersion { get; } 
    IEnumerable<EntityDefinition> EntityDefinitions { get; }
    NamespacedKey? PlanetDefinition { get; }
    IEnumerable<WeaponDefinition> Weapons { get; }



    // Methods.
    EntityDefinition? GetEntityDefinition(NamespacedKey key);
    WeaponDefinition? GetWeaponDefinition(NamespacedKey key);
}