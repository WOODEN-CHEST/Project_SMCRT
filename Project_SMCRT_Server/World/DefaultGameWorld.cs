using GHEngine;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World.Component;
using Project_SMCRT_Server.World.Component.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class DefaultGameWorld : IGameWorld
{
    // Fields.
    public double SimulationSpeed
    {
        get => _time.SimulationSpeed;
        set => _time.SimulationSpeed = value;
    }

    public bool IsSimulationRunning { get; set; }

    public IDataPack UsedDataPack
    {
        get => _usedDataPack;
        set => _usedDataPack = value ?? throw new ArgumentNullException(nameof(value));
    }


    public IEnumerable<ulong> Entities => _entities.Entities;
    public IEnumerable<EntityComponent> Components => _entities.Components;


    // Private fields.
    private IDataPack _usedDataPack;

    private readonly IEntityCollection _entities = new DefaultEntityCollection();
    private readonly WorldTime _time = new WorldTime();
    private readonly List<Action> _scheduledActions = new();
    private readonly List<IComponentSystem> _systems = new();



    // COnstructors.
    public DefaultGameWorld(IDataPack usedDataPack)
    {
        UsedDataPack = usedDataPack;
    }


    // Private methods.
    private void RunSimulation()
    {
        foreach (IComponentSystem System in _systems)
        {
            System.Execute(this, _time.VirtualTime);
        }
    }


    // Methods.
    public bool AddComponent(ulong entity, EntityComponent component)
    {
        return _entities.AddComponent(entity, component);
    }

    public bool ClearComponents(ulong entity)
    {
        return _entities.ClearComponents(entity);
    }

    public bool CreateEntity(ulong id, params EntityComponent[] components)
    {
        return _entities.CreateEntity(id, components);
    }

    public EntityComponent? GetComponent(ulong entity, NamespacedKey key)
    {
        return _entities.GetComponent(entity, key);
    }

    public IEnumerable<EntityComponent>? GetComponents(ulong entity)
    {
       return _entities.GetComponents(entity);
    }

    public IEnumerable<EntityComponent>? GetComponents(NamespacedKey key)
    {
        return _entities.GetComponents(key);
    }

    public bool RemoveComponent(ulong entity, NamespacedKey key)
    {
        return _entities.RemoveComponent(entity, key);
    }

    public bool RemoveEntity(ulong id)
    {
        return _entities.RemoveEntity(id);
    }

    public void ScheduleAction(Action action)
    {
        _scheduledActions.Add(action ?? throw new ArgumentNullException(nameof(action)));
    }

    public void Update(IProgramTime time)
    {
        _time.Update(time);

        foreach (Action ScheduledAction in _scheduledActions)
        {
            ScheduledAction.Invoke();
        }

        if (IsSimulationRunning)
        {
            RunSimulation();
        }
    }
}