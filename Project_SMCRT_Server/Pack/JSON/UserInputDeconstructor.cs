using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class UserInputDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_IS_INPUT_ENABLED = "enable_input";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        UserInputComponent InputComponent = new();

        if (compound.GetOptionalVerified(KEY_IS_INPUT_ENABLED, out bool IsInputEnabled))
        {
            InputComponent.IsInputRegistered = IsInputEnabled;
        }

        return InputComponent;
    }
}