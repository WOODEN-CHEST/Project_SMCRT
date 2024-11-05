using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class MotionDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_MOTION = "motion";
    public const string KEY_ANGULAR_MOTION = "angular_motion";


    // Methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        MotionComponent Component = new();

        if (compound.GetOptionalVerified<JSONList>(KEY_MOTION, out JSONList? PositionList))
        {
            Component.Motion = genericDeconstructor.GetVector(PositionList!);
        }
        if (compound.GetOptionalVerified<object>(KEY_ANGULAR_MOTION, out object? Rotation))
        {
            Component.AngularMotion = genericDeconstructor.GetAsDouble(Rotation!);
        }

        return Component;
    }
}