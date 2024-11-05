using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class MotionComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("motion");


    // Fields.
    public DVector2 Motion { get; set; }
    public double AngularMotion { get; set; }


    // Constructors.
    public MotionComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new MotionComponent()
        {
            Motion = Motion,
            AngularMotion = AngularMotion
        };
    }
}