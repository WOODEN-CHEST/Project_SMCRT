using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class PositionDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_POSITION = "position";
    public const string KEY_ROTATION = "rotation";


    // Methods. 
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        PositionComponent Component = new();

        if (compound.GetOptionalVerified<JSONList>(KEY_POSITION, out JSONList? PositionList))
        {
            Component.Position = new(genericDeconstructor.GetAsDouble(PositionList!.GetVerified<object>(0)),
                genericDeconstructor.GetAsDouble(PositionList!.GetVerified<object>(0)!));
        }
        if (compound.GetOptionalVerified<object>(KEY_ROTATION, out object? Rotation))
        {
            Component.Rotation = genericDeconstructor.GetAsDouble(Rotation!);
        }

        return Component;
    }
}