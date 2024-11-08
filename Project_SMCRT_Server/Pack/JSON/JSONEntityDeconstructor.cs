using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class JSONEntityDeconstructor
{
    // Static fields.
    public const string KEY_KEY = "key";
    public const string KEY_COMPONENTS = "components";


    // Private fields.
    private readonly JSONComponentDeconstructor _componentDeconstructor = new();


    // Private methods.
    private NamespacedKey GetEntityKey(JSONCompound compound)
    {
        string Key = compound.GetVerified<string>(KEY_KEY);

        try
        {
            return new NamespacedKey(Key);
        }
        catch (ArgumentException)
        {
            throw new PackContentException($"Invalid entity key: \"{Key}\"");
        }
    }

    private EntityComponent[] GetComponents(JSONCompound compound)
    {
        List<EntityComponent> ParsedComponents = new();
        JSONList TargetList = compound.GetVerified<JSONList>(KEY_COMPONENTS);

        foreach (object? ListObject in TargetList)
        {
            if (ListObject is not JSONCompound ComponentCompound)
            {
                throw new PackContentException("Got non-compound object in entity component list definitions.");
            }

            ParsedComponents.Add(_componentDeconstructor.GetComponent(ComponentCompound));
        }

        return ParsedComponents.ToArray();
    }


    // Methods.
    public EntityDefinition GetDefinition(JSONCompound compound)
    {
        ArgumentNullException.ThrowIfNull(compound, nameof(compound));

        NamespacedKey Key = GetEntityKey(compound);
        EntityComponent[] Components = GetComponents(compound);

        return new EntityDefinition(Key, Components);
    }

    public EntitySpawnProperties GetSpawnProperties(JSONCompound compound)
    {
        ArgumentNullException.ThrowIfNull(compound, nameof(compound));

        NamespacedKey Key = GetEntityKey(compound);
        EntityComponent[] Components = GetComponents(compound);

        return new EntitySpawnProperties(Key, Components);
    }
}