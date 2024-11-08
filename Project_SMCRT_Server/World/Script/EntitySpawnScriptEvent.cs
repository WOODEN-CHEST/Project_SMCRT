using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Script;

public class EntitySpawnScriptEvent : IScriptEvent
{
    // Fields.
    public TimeSpan TriggerTime { get; private set; }
    public NamespacedKey Entity { get; private set; }
    public EntityComponent[] StartingComponents { get; private set; }


    // Constructors.
    public EntitySpawnScriptEvent(TimeSpan triggerTime, NamespacedKey entityKey, EntityComponent[] presetComponents)
    {
        TriggerTime = triggerTime;
        Entity = entityKey ?? throw new ArgumentNullException(nameof(entityKey));
        StartingComponents = presetComponents ?? throw new ArgumentNullException(nameof(presetComponents));
    }


    // Inherited methods.
    public void ExecuteEvent(IGameWorld world)
    {
        EntityDefinition? Definition = world.UsedDataPack.GetEntityDefinition(Entity);
        if (Definition == null)
        {
            return;
        }

        world.CreateEntity(new HashSet<EntityComponent>(StartingComponents.Concat(Definition.StartingComponents)).ToArray());
    }
}