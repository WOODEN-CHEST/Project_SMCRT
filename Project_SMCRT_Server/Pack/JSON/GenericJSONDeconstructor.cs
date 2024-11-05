using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class GenericJSONDeconstructor
{
    // Static fields.
    public const string KEY_INPUT_ACTION_ID = "id";


    // Methods.
    public double GetAsDouble(object value)
    {
        if (value is long)
        {
            return (double)(long)value;
        }
        if (value is double)
        {
            return (double)value;
        }
        throw new PackContentException("Expected number, got invalid bullshit. I am tired of writing these error messages " +
            "which don't really even help at this point..");
    }

    public InputAction GetInputAction(JSONCompound compound)
    {
        return new InputAction((int)compound.GetVerified<long>(KEY_INPUT_ACTION_ID));
    }
}