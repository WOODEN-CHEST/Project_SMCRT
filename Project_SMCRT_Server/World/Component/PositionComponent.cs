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
    public static readonly NamespacedKey KEY = new(NamespacedKey.NAMESPACE_SMCRT, "position");


    // Fields.
    public DVector2 Position { get; set; }
    public double Rotation { get; set; }


    // Constructors.
    public PositionComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new PositionComponent()
        {
            Position = Position,
            Rotation = Rotation
        };
    }
}