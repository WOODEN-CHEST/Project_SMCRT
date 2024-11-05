using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Monitor;

public class DefaultWorldMonitor : IWorldMonitor
{
    // Fields.
    public IGameWorld MonitoredWorld => _world;

    public IEnumerable<AddedEntity> EntitiesAdded => 
        _entitiesAdded.Select(pair => new AddedEntity(pair.Key, pair.Value.ToArray()));

    public IEnumerable<RemovedEntity> EntitiesRemoved => 
        _entitiesRemoved.Select(entity => new RemovedEntity(entity));

    public IEnumerable<AddedComponent> ComponentsAdded =>
        _componentsAdded.Select(pair => new AddedComponent(pair.Key, pair.Value.ToArray()));

    public IEnumerable<RemovedComponent> ComponentsRemoved =>
        _componentsRemoved.Select(pair => new RemovedComponent(pair.Key, pair.Value.ToArray()));

    public IEnumerable<UpdatedComponent> ComponentsUpdated =>
        _componentsChanged.Select(pair => new UpdatedComponent(pair.Key, pair.Value.ToArray()));

    public double? NewSimulationSpeed { get; private set; } = null;

    public bool? IsSimulationPaused { get; private set; } = null;


    // Private fields.
    private readonly IGameWorld _world;

    private readonly Dictionary<ulong, HashSet<EntityComponent>> _entitiesAdded = new();
    private readonly HashSet<ulong> _entitiesRemoved = new();
    private readonly Dictionary<ulong, HashSet<EntityComponent>> _componentsChanged = new();
    private readonly Dictionary<ulong, HashSet<EntityComponent>> _componentsAdded = new();
    private readonly Dictionary<ulong, HashSet<EntityComponent>> _componentsRemoved = new();


    // Constructors.
    public DefaultWorldMonitor(IGameWorld world)
    {
        _world = world ?? throw new ArgumentNullException(nameof(world));
    }


    // Private methods.
    private void OnSimulationSpeedChangeEvent(object? sender, SimulationSpeedChangeArgs args)
    {
        NewSimulationSpeed = args.NewSimulationSpeed;
    }

    private void OnPauseChangeEvent(object? sender, PauseChangeArgs args)
    {
        IsSimulationPaused = args.IsPaused;
    }

    private void OnEntityAddEvent(object? sender, EntityAddEventArgs args)
    {
        _entitiesAdded[args.Entity] = new(args.Components);
    }

    private void OnEntityRemoveEvent(object? sender, EntityRemoveEventArgs args)
    {
        _entitiesRemoved.Add(args.Entity);
    }

    private void OnComponentAddEvent(object? sender, ComponentAddEventArgs args)
    {
        if (!_componentsAdded.TryGetValue(args.Entity, out var Components))
        {
            Components = new();
            _componentsAdded[args.Entity] = Components;
        }

        Components.Add(args.Component);
    }

    private void OnComponentChangeEvent(object? sender, ComponentUpdateEventArgs args)
    {
        if (!_componentsChanged.TryGetValue(args.Entity, out var Components))
        {
            Components = new();
            _componentsChanged[args.Entity] = Components;
        }

        Components.Add(args.Component);
    }

    private void OnComponentRemoveEvent(object? sender, ComponentRemoveEventArgs args)
    {
        if (!_componentsRemoved.TryGetValue(args.Entity, out var Components))
        {
            Components = new();
            _componentsRemoved[args.Entity] = Components;
        }

        Components.Add(args.Component);
    }


    // Methods.
    public void ResetMonitors()
    {
        NewSimulationSpeed = null;
        IsSimulationPaused = null;
        _entitiesAdded.Clear();
        _entitiesRemoved.Clear();
        _componentsChanged.Clear();
        _componentsAdded.Clear();
        _componentsRemoved.Clear();
    }

    public void StartMonitor()
    {

    }

    public void EndMonitor()
    {

    }
}