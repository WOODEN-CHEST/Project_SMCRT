using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class DefaultEntityCollection : IEntityCollection
{
    // Fields.
    public IEnumerable<ulong> Entities => _componentsByEntity.Keys;

    public IEnumerable<EntityComponent> Components => _combinedByType.Values.SelectMany(components => components);


    // Private fields.
    private readonly Dictionary<ulong, Dictionary<NamespacedKey, EntityComponent>> _componentsByEntity = new();
    private readonly Dictionary<NamespacedKey, HashSet<EntityComponent>> _combinedByType = new();


    // Private methods. 
    private Dictionary<NamespacedKey, EntityComponent> EnsureEntityDict(ulong id)
    {
        if (_componentsByEntity.TryGetValue(id, out Dictionary<NamespacedKey, EntityComponent>? Components))
        {
            return Components;
        }
        Dictionary<NamespacedKey, EntityComponent> Dict = new();
        _componentsByEntity[id] = Dict;
        return Dict;
    }

    private HashSet<EntityComponent> EnsureTypeSet(NamespacedKey key)
    {
        if (_combinedByType.TryGetValue(key, out HashSet<EntityComponent>? Components))
        {
            return Components;
        }

        HashSet<EntityComponent> Set = new();
        _combinedByType[key] = Set;
        return Set;
    }


    // Inherited methods.
    public bool AddComponent(ulong entity, EntityComponent component)
    {
        ArgumentNullException.ThrowIfNull(component, nameof(component));
        if (!_componentsByEntity.TryGetValue(entity, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return false;
        }

        if (EntityComponents.ContainsKey(component.Key))
        {
            return false;
        }

        EntityComponents[component.Key] = component;
        EnsureTypeSet(component.Key).Add(component);

        return true;
    }

    public bool ClearComponents(ulong entity)
    {
        if (!_componentsByEntity.TryGetValue(entity, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return false;
        }

        foreach (EntityComponent Component in EntityComponents.Values)
        {
            _combinedByType[Component.Key].Remove(Component);
        }
        EntityComponents.Clear();
        return true;
    }
    public EntityComponent? GetComponent(ulong entity, NamespacedKey key)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        if (!_componentsByEntity.TryGetValue(entity, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return null;
        }
        EntityComponents.TryGetValue(key, out EntityComponent? Component);
        return Component;
    }

    public IEnumerable<EntityComponent>? GetComponents(ulong entity)
    {
        if (!_componentsByEntity.TryGetValue(entity, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return null;
        }
        return EntityComponents.Values;
    }

    public bool RemoveComponent(ulong entity, NamespacedKey key)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        if (!_componentsByEntity.TryGetValue(entity, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return false;
        }

        if (EntityComponents.TryGetValue(key, out EntityComponent? RemovedComponent))
        {
            EntityComponents.Remove(key);
            _combinedByType[key].Remove(RemovedComponent);
            return true;
        }
        return false;
    }

    public IEnumerable<EntityComponent>? GetComponents(NamespacedKey key)
    {
        _combinedByType.TryGetValue(key, out HashSet<EntityComponent>? Components);
        return Components;
    }

    public bool CreateEntity(ulong id, params EntityComponent[] components)
    {
        if (_componentsByEntity.ContainsKey(id))
        {
            return false;
        }

        Dictionary<NamespacedKey, EntityComponent> EntityComponents = EnsureEntityDict(id);
        foreach (EntityComponent Component in components)
        {
            ArgumentNullException.ThrowIfNull(Component, nameof(components));
            EntityComponents.Add(Component.Key, Component);
            EnsureTypeSet(Component.Key).Add(Component);
        }
        return true;
    }

    public bool RemoveEntity(ulong id)
    {
        if (!_componentsByEntity.TryGetValue(id, out Dictionary<NamespacedKey, EntityComponent>? EntityComponents))
        {
            return false;
        }

        foreach (EntityComponent Component in EntityComponents.Values)
        {
            _combinedByType[Component.Key].Remove(Component);
        }
        _componentsByEntity.Remove(id);
        return true;
    }


    // Types.
    private readonly record struct EntityToAdd(ulong EntityID, EntityComponent[] Components);
}