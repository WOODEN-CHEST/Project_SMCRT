using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public interface IEntityCollection
{
    // Fields.
    IEnumerable<ulong> Entities { get; }
    IEnumerable<EntityComponent> Components { get; }


    // Methods.
    bool CreateEntity(ulong id, params EntityComponent[] components);
    bool RemoveEntity(ulong id);
    IEnumerable<EntityComponent>? GetComponents(ulong entity);
    IEnumerable<EntityComponent>? GetComponents(NamespacedKey key);
    EntityComponent? GetComponent(ulong entity, NamespacedKey key);
    bool RemoveComponent(ulong entity, NamespacedKey key);
    bool ClearComponents(ulong entity);
    bool AddComponent(ulong entity, EntityComponent component);
}