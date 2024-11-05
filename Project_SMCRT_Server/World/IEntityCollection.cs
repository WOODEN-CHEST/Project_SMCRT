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

    event EventHandler<ComponentAddEventArgs>? ComponentAdd;
    event EventHandler<ComponentRemoveEventArgs>? ComponentRemove;
    event EventHandler<EntityAddEventArgs>? EntityAdd;
    event EventHandler<EntityRemoveEventArgs>? EntityRemove;


    // Methods.
    bool CreateEntity(ulong id, params EntityComponent[] components);
    bool RemoveEntity(ulong id);
    IEnumerable<T> GetComponents<T>(ulong entity) where T : EntityComponent;
    IEnumerable<T> GetComponents<T>(NamespacedKey key) where T : EntityComponent;
    T? GetComponent<T>(ulong entity, NamespacedKey key) where T : EntityComponent;
    bool RemoveComponent(ulong entity, NamespacedKey key);
    bool ClearComponents(ulong entity);
    bool AddComponent(ulong entity, EntityComponent component);
    ulong? GetEntityOfComponent(EntityComponent component);
}