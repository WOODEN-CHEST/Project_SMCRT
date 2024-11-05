using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

internal class MotionSystem : IComponentSystem
{
    // Fields.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (MotionComponent EntityMotion in world.GetComponents<MotionComponent>(MotionComponent.KEY))
        {
            ulong Entity = world.GetEntityOfComponent(EntityMotion)!.Value;

            PositionComponent? EntityPosition = world.GetComponent<PositionComponent>(Entity, PositionComponent.KEY);
            if (EntityPosition != null)
            {
                EntityPosition.Position += EntityMotion.Motion * time.PassedTime.TotalSeconds;
                EntityPosition.Rotation += EntityMotion.AngularMotion * time.PassedTime.TotalSeconds;
                ComponentUpdate?.Invoke(this, new(EntityPosition, Entity));
            }
        }
    }
}