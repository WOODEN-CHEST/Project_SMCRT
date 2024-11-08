using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public class ResourceContainerSystem : IComponentSystem
{
    // Fields.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (ResourceContainerComponent Component in world.GetComponents<ResourceContainerComponent>(ResourceContainerComponent.KEY))
        {
            bool Changed = false;
            if (Component.CompositesPerSecond != 0d)
            {
                Component.Composites += Component.CompositesPerSecond * time.PassedTime.TotalSeconds;
                Changed = true;
            }
            if (Component.FuelPerSecond != 0d)
            {
                Component.Fuel += Component.FuelPerSecond * time.PassedTime.TotalSeconds;
                Changed = true;
            }
            if (Component.ResearchPerSecond != 0d)
            {
                Component.Research += Component.ResearchPerSecond * time.PassedTime.TotalSeconds;
                Changed = true;
            }
            if (Component.MetalPerSecond != 0d)
            {
                Component.Metal += Component.MetalPerSecond * time.PassedTime.TotalSeconds;
                Changed = true;
            }

            if (Changed)
            {
                ComponentUpdate?.Invoke(this, new(Component, world.GetEntityOfComponent(Component)!.Value));
            }
        }
    }
}