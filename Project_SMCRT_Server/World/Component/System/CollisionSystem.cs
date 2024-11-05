using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public class CollisionSystem : IComponentSystem
{
    // Static fields.
    public static readonly double DEFAULT_MASS = 1000d;


    // Constructors.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Private fields.
    private readonly ForceApplicator _forceApplicator = new();

    // Private methods.


    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (CollisionMeshComponent EntityMesh in world.GetComponents<CollisionMeshComponent>(CollisionMeshComponent.KEY))
        {
            ulong Entity = world.GetEntityOfComponent(EntityMesh)!.Value;

            PositionComponent? EntityPositionComponent = world.GetComponent<PositionComponent>(Entity, PositionComponent.KEY);
            MotionComponent? EntityMotionComponent = world.GetComponent<MotionComponent>(Entity, MotionComponent.KEY);
            DVector2 Position = EntityPositionComponent?.Position ?? DVector2.Zero;
            double Rotation = EntityPositionComponent?.Rotation ?? 0d;
            DVector2 Motion = EntityMotionComponent?.Motion ?? DVector2.Zero;


        }
    }


    // Types.
    private record class CollisionCase(ulong EntityA,
        ulong EntityB, 
        DVector2 AverageCollisionPoint);
}