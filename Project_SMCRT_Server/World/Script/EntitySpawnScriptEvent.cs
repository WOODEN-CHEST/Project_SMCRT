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
    public TimeSpan TriggerTime => throw new NotImplementedException();


    // Constructors.
    public EntitySpawnScriptEvent(TimeSpan triggerTime, NamespacedKey entityKey, EntityComponent[] presetComponents)
    {

    }


    // Inherited methods.
    public void ExecuteEvent(IGameWorld world)
    {
        throw new NotImplementedException();
    }
}