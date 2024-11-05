using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public interface IComponentSystem
{
    // Fields.
    event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;
    

    // Methods.
    void Execute(IGameWorld world, IProgramTime time);
}