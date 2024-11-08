using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class EntityThruster
{
    // Static fields.
    public const double STRENGTH_DEFAULT = 0d;
    public const double STRENGTH_MIN = -1e18d;
    public const double STRENGTH_MAX = 1e18d;


    // Fields.
    public bool FollowsRotation { get; set; }

    public double Strength
    {
        get => _strength;
        set => _strength = double.IsNaN(value) ? STRENGTH_DEFAULT : Math.Clamp(value, STRENGTH_MIN, STRENGTH_MAX);
    }

    public InputAction ActivationInputAction { get; set; } = InputAction.None;

    public double Rotation { get; set; } = 0d;

    public DVector2 Offset { get; set; } = DVector2.Zero;


    // Private fields.
    private double _strength = STRENGTH_DEFAULT;



    // Methods.
    public EntityThruster CreateCopy()
    {
        return new EntityThruster()
        {
            FollowsRotation = FollowsRotation,
            Strength = Strength,
            ActivationInputAction = ActivationInputAction,
            Rotation = Rotation
        };
    }
}