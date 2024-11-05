using Project_SMCRT_Server.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.Script;

public interface IScriptEvent
{
    // Fields.
    public TimeSpan TriggerTime { get; }



    // Methods.
    void ExecuteEvent(IGameWorld world);
}