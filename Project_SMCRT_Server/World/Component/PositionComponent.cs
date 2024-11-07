using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class PositionComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("position");


    // Fields.
    public DVector2 Position { get; set; }
    public double Rotation { get; set; }


    // Constructors.
    public PositionComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        PositionComponent NewComponent = new();
        NewComponent.SetFrom(this);
        return NewComponent;
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not PositionComponent Target)
        {
            return false;
        }

        Position = Target.Position;
        Rotation = Target.Rotation;

        return true;
    }
}