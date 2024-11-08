using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class WeaponDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_KEY = "key";
    public const string KEY_INPUT_ACTION = "input_action";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        WeaponComponent InputComponent = new();

        try
        {
            InputComponent.WeaponKey = new NamespacedKey(compound.GetVerified<string>(KEY_KEY));
        }
        catch (ArgumentException e)
        {
            throw new PackContentException("Invalid key for weapon");
        }
        
        if (compound.GetOptionalVerified(KEY_INPUT_ACTION, out JSONCompound? InputCompound))
        {
            InputComponent.RequiredInputAction = genericDeconstructor.GetInputAction(InputCompound!);
        }

        return InputComponent;
    }
}