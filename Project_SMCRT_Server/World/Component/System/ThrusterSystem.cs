using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public class ThrusterSystem : IComponentSystem
{
    // Fields.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Private fields.
    private readonly ForceApplicator _forceApplicator = new();


    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (ThrusterComponent ThrusterComp in world.GetComponents<ThrusterComponent>(ThrusterComponent.KEY))
        {
            ulong Entity = world.GetEntityOfComponent(ThrusterComp)!.Value;

            PhysicalPropertiesComponent? PhysicalProperties =
                world.GetComponent<PhysicalPropertiesComponent>(Entity, PhysicalPropertiesComponent.KEY);
            MotionComponent? Motion = world.GetComponent<MotionComponent>(Entity, MotionComponent.KEY);
            UserInputComponent? UserInput = world.GetComponent<UserInputComponent>(Entity, UserInputComponent.KEY);
            PositionComponent? Position = world.GetComponent<PositionComponent>(Entity, PositionComponent.KEY);

            if ((Motion == null) || (PhysicalProperties == null) || (UserInput == null) || (Position == null))
            {
                continue;
            }

            foreach (EntityThruster Thruster in ThrusterComp.Thrusters)
            {
                if (UserInput.IsActionActive(Thruster.ActivationInputAction))
                {
                    DVector2 Force = DVector2.Rotate(new DVector2(1f, 0f),
                        (Thruster.FollowsRotation ? Position.Rotation : 0d) + Thruster.Rotation) * Thruster.Strength;
                    if (Force.LengthSquared == 0d)
                    {
                        continue;
                    }

                    _forceApplicator.ApplyForce(Position, Motion, PhysicalProperties, Force, Position.Position + Thruster.Offset);
                    ComponentUpdate?.Invoke(this, new(Motion, Entity));
                }
            }
        }
    }
}