using GHEngine;
using Project_SMCRT_Server.Pack;


namespace Project_SMCRT_Server.World.Component.System;


public class WeaponSystem : IComponentSystem
{
    // Fields.
    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;


    // Private fields.
    private IIDProvider _entityIDProvider;


    // Constructors.
    public WeaponSystem(IIDProvider entityIDProvider)
    {
        _entityIDProvider = entityIDProvider ?? throw new ArgumentNullException(nameof(entityIDProvider));
    }


    // Private methods.
    private void ReloadAmmo(IGameWorld world, WeaponComponent component, WeaponDefinition definition, IProgramTime time, ulong entity)
    {
        component.ReloadProgress += time.PassedTime;
        if (component.ReloadProgress >= definition.ReloadTime)
        {
            component.AmmoLeft = definition.CartridgeSize;
            component.ReloadProgress = TimeSpan.Zero;
            if (definition.ReloadSounds.Length > 0)
            {
                PositionComponent SelfPosition = world.GetComponent<PositionComponent>(entity, PositionComponent.KEY)
                    ?? new PositionComponent();

                world.PlaySound(definition.ReloadSounds[Random.Shared.Next(definition.ReloadSounds.Length)],
                    SelfPosition.Position, 1d, 1f, null);
            }
        }
    }

    private void Shoot(IGameWorld world, ulong entity, WeaponComponent component, WeaponDefinition definition, IProgramTime time)
    {
        // I dont event care about code quality anymore.
        PositionComponent SelfPosition = world.GetComponent<PositionComponent>(entity, PositionComponent.KEY) ?? new PositionComponent();

        EntityDefinition? EntityToCreateDefinition = world.UsedDataPack.GetEntityDefinition(definition.EntityKey);
        if (EntityToCreateDefinition == null)
        {
            return;
        }
        EntityComponent[] CreatedComponents = new HashSet<EntityComponent>(definition.EntityStartingComponents
            .Concat(EntityToCreateDefinition.StartingComponents)).ToArray();

        PositionComponent? EntityPosition = (PositionComponent?)CreatedComponents.
            Where(component => component.Key.Equals(PositionComponent.KEY)).FirstOrDefault();
        MotionComponent? EntityMotion = (MotionComponent?)CreatedComponents.
            Where(component => component.Key.Equals(MotionComponent.KEY)).FirstOrDefault();
        CollisionMeshComponent? EntityCollisionMesh = (CollisionMeshComponent?)CreatedComponents.
            Where(component => component.Key.Equals(CollisionMeshComponent.KEY)).FirstOrDefault();

        if ((EntityPosition != null) && (EntityMotion != null))
        {
            EntityPosition.Position = SelfPosition.Position;
            EntityMotion.Motion = DVector2.Rotate(new DVector2(1d, 0d), SelfPosition.Rotation
                + (Random.Shared.NextDouble() * definition.SpreadAngle + definition.AngleOffset));
        }

        component.AmmoLeft--;
        component.TimeSinceWeaponFire = TimeSpan.Zero;
        ulong NewEntityID = _entityIDProvider.GetID();

        if (EntityCollisionMesh != null)
        {
            EntityCollisionMesh.ExcludeCollision(NewEntityID);
        }

        if (definition.ShootSounds.Length > 0)
        {
            world.PlaySound(definition.ShootSounds[Random.Shared.Next(definition.ShootSounds.Length)],
                   SelfPosition.Position, 1d, 1f, null);
        }

        if (world.IsEntityPlayer(entity) && (EntityCollisionMesh != null))
        {
            foreach (ulong PlayerEntity in world.PlayersEntities)
            {
                EntityCollisionMesh.ExcludeCollision(PlayerEntity);
            }
        }
        
        world.ScheduleAction(() => world.CreateEntity(NewEntityID, CreatedComponents));
    }



    // Inherited methods.
    public void Execute(IGameWorld world, IProgramTime time)
    {
        foreach (WeaponComponent Component in world.GetComponents<WeaponComponent>(WeaponComponent.KEY))
        {
            ulong Entity = world.GetEntityOfComponent(Component)!.Value;
            UserInputComponent? Input = world.GetComponent<UserInputComponent>(Entity, UserInputComponent.KEY);
            WeaponDefinition? Definition = world.UsedDataPack.GetWeaponDefinition(Component.WeaponKey);
            if ((Input == null) || (Definition == null))
            {
                continue;
            }

            if (Component.AmmoLeft == 0)
            {
                ReloadAmmo(world, Component, Definition, time, Entity);
            }
            else if (Input.IsActionJustNowInactive(Component.RequiredInputAction) 
                && Component.TimeSinceWeaponFire < Definition.DelayBetweenShots
                && Component.AmmoLeft > 0)
            {
                Shoot(world, Entity, Component, Definition, time);
            }

            ComponentUpdate?.Invoke(this, new(Component, Entity));
        }
    }
}