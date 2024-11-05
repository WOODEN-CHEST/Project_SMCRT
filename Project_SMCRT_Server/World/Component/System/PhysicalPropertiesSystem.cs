using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component.System;

public class PhysicalPropertiesSystem : IComponentSystem
{
    // Static fields.
    public const double HEAT_FALLOFF_PER_SECOND = 0.995d;
    public const double MATERIAL_FIRE_FALLOFF_PER_SECOND = 0.97d;
    public const double MATERIAL_FIRE_SCALE_PER_KELVIN = 0.05d;


    // Fields.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Private methods.
    private void DealFireDamage(PhysicalPropertiesComponent component)
    {
        component.Health *= MATERIAL_FIRE_FALLOFF_PER_SECOND * (component.Temperature - component.FlameStartTemperature)
            * (1d - component.FireResistance) * MATERIAL_FIRE_SCALE_PER_KELVIN;
    }

    private void AttractEntities(IGameWorld world,
        IProgramTime time, 
        PhysicalPropertiesComponent selfProperties,
        PositionComponent selfPosition,
        ulong selfEntity)
    {
        foreach (MotionComponent TargetMotion in world.GetComponents<MotionComponent>(MotionComponent.KEY))
        {
            ulong TargetEntity = world.GetEntityOfComponent(TargetMotion)!.Value;
            if (TargetEntity == selfEntity)
            {
                continue;
            }

            PositionComponent? TargetPosition = world.GetComponent<PositionComponent>(TargetEntity, PositionComponent.KEY);
            if (TargetPosition == null)
            {
                continue;
            }

            double DistanceToEntity = Math.Max(double.Epsilon, (TargetPosition.Position - selfPosition.Position).Length);
            TargetMotion.Motion += selfProperties.Attraction / (DistanceToEntity * DistanceToEntity) * time.TotalTime.TotalSeconds;
            ComponentUpdate?.Invoke(this, new(TargetMotion, TargetEntity));
        }
    }


    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (PhysicalPropertiesComponent PhysicalProperty in 
            world.GetComponents<PhysicalPropertiesComponent>(PhysicalPropertiesComponent.KEY))
        {
            ulong Entity = world.GetEntityOfComponent(PhysicalProperty)!.Value;
            PhysicalProperty.Temperature *= HEAT_FALLOFF_PER_SECOND * time.TotalTime.TotalSeconds / PhysicalProperty.HeatCapacity;
            PhysicalProperty.IsOnFire = (PhysicalProperty.Temperature >= PhysicalProperty.FlameStartTemperature)
                && (PhysicalProperty.FireResistance < 1d);
            if (PhysicalProperty.IsOnFire)
            {
                DealFireDamage(PhysicalProperty);
            }

            PositionComponent? SelfPosition = world.GetComponent<PositionComponent>(Entity, PositionComponent.KEY);
            if ((SelfPosition != null) && (PhysicalProperty.Attraction > 0d))
            {
                AttractEntities(world, time, PhysicalProperty, SelfPosition, Entity);
            }
            ComponentUpdate?.Invoke(this, new(PhysicalProperty, Entity));
        }
    }
}