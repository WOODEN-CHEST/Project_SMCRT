﻿using GHEngine;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World.Component;
using Project_SMCRT_Server.World.Component.System;

namespace Project_SMCRT_Server.World;

public class DefaultGameWorld : IGameWorld
{
    // Fields.
    public ulong ID { get; private init; }

    public double SimulationSpeed
    {
        get => _time.SimulationSpeed;
        set
        {
            _time.SimulationSpeed = value;
            SimulationSpeedChange.Invoke(this, new(_time.SimulationSpeed));
        }
    }

    public bool IsSimulationPaused
    {
        get => _isSimulationPaused;
        set
        {
            _isSimulationPaused = value;
            PauseChange.Invoke(this, new(_isSimulationPaused));
        }
    }

    public ulong Planet { get; private set; }

    public IDataPack UsedDataPack { get; private init; }

    public IEnumerable<ulong> Entities => _entities.Entities;
    public IEnumerable<EntityComponent> Components => _entities.Components;

    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;
    public event EventHandler<ComponentAddEventArgs>? ComponentAdd;
    public event EventHandler<ComponentRemoveEventArgs>? ComponentRemove;
    public event EventHandler<EntityAddEventArgs>? EntityAdd;
    public event EventHandler<EntityRemoveEventArgs>? EntityRemove;
    public event EventHandler<SimulationSpeedChangeArgs> SimulationSpeedChange;
    public event EventHandler<PauseChangeArgs> PauseChange;


    // Private fields.
    private bool _isSimulationPaused = false;

    private readonly IIDProvider _entityIDProvider = new SequentialIDProvider();
    private readonly IEntityCollection _entities = new DefaultEntityCollection();
    private readonly WorldTime _time = new WorldTime();
    private readonly List<Action> _scheduledActions = new();
    private readonly List<IComponentSystem> _systems = new();


    // COnstructors.
    public DefaultGameWorld(ulong id, IDataPack usedDataPack)
    {
        ID = id;
        UsedDataPack = usedDataPack ?? throw new ArgumentNullException(nameof(usedDataPack));
        CreatePlanet();
        InitializeSystems();
    }


    // Private methods.
    private void CreatePlanet()
    {
        NamespacedKey? PlanetKey = UsedDataPack.PlanetDefinition;
        if (PlanetKey == null)
        {
            throw new PackContentException("No planet defined.");
        }
        EntityDefinition PlanetDefinition = UsedDataPack.GetEntityDefinition(PlanetKey)
            ?? throw new PackContentException("Entity definition for defined planet not found");

        ulong PlanetID = _entityIDProvider.GetID();
        _entities.CreateEntity(PlanetID, PlanetDefinition.StartingComponents
            .Select(component => component.CreateCopy()).ToArray());
        Planet = PlanetID;
    }

    private void InitializeSystems()
    {
        // Order here matters.
        _systems.Add(new MotionSystem());

        foreach (IComponentSystem System in _systems)
        {
            System.ComponentUpdate += OnComponentUpdateEvent;
        }
    }

    private void RunSimulation()
    {
        foreach (IComponentSystem System in _systems)
        {
            System.Execute(this, _time.VirtualTime);
        }
    }

    private void OnEntityAddEvent(object? sender, EntityAddEventArgs args)
    {
        EntityAdd?.Invoke(sender, args);
    }

    private void OnEntityRemoveEvent(object? sender, EntityRemoveEventArgs args)
    {
        EntityRemove?.Invoke(sender, args);
    }

    private void OnComponentAddEvent(object? sender, ComponentAddEventArgs args)
    {
        ComponentAdd?.Invoke(sender, args);
    }

    private void OnComponentRemoveEvent(object? sender, ComponentRemoveEventArgs args)
    {
        ComponentRemove?.Invoke(sender, args);
    }

    private void OnComponentUpdateEvent(object? sender, ComponentUpdateEventArgs args)
    {
        ComponentUpdate?.Invoke(sender, args);
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

    public T? GetComponent<T>(ulong entity, NamespacedKey key) where T : EntityComponent
    {
        return _entities.GetComponent<T>(entity, key);
    }

    public IEnumerable<T> GetComponents<T>(ulong entity) where T : EntityComponent
    {
       return _entities.GetComponents<T>(entity);
    }

    public IEnumerable<T> GetComponents<T>(NamespacedKey key) where T : EntityComponent
    {
        return _entities.GetComponents<T>(key);
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

        if (!IsSimulationPaused)
        {
            RunSimulation();
        }
    }

    public ulong? GetEntityOfComponent(EntityComponent component)
    {
        return _entities.GetEntityOfComponent(component);
    }
}