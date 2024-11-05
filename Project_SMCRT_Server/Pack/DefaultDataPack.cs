using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public class DefaultDataPack : IModifiableDataPack
{
    // Fields.
    public IEnumerable<EntityDefinition> EntityDefinitions => _definedEntities.Values;
    public NamespacedKey? PlanetDefinition { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Version TargetedGameVersion { get; private set; }


    // Private fields.
    private readonly Dictionary<NamespacedKey, EntityDefinition> _definedEntities = new();


    // Constructors.
    public DefaultDataPack() { }


    // Inherited methods.
    public void AddEntityDefinition(EntityDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition, nameof(definition));
        _definedEntities.Add(definition.Key, definition);
    }

    public EntityDefinition? GetEntityDefinition(NamespacedKey key)
    {
        _definedEntities.TryGetValue(key, out var EntityDefinition);
        return EntityDefinition;
    }

    public void SetPlanet(NamespacedKey planet)
    {
         PlanetDefinition = planet ?? throw new ArgumentNullException(nameof(planet));
    }

    public void SetName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void SetDescription(string description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public void SetVersion(Version version)
    {
        TargetedGameVersion = version ?? throw new ArgumentNullException(nameof(version));
    }
}