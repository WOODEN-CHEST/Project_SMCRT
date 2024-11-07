using GHEngine;
using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class JSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_KEY = "key";




    // Private fields.
    private readonly GenericJSONDeconstructor _genericDeconstructor = new();
    private readonly Dictionary<NamespacedKey, IJSONComponentDeconstructor> _deconstructors = new()
    {

    };



    // Private methods.
    /* Generic parsing. */



    /* Component parsing. */
    private NamespacedKey GetComponentKey(JSONCompound compound)
    {
        string Key = compound.GetVerified<string>(KEY_KEY);

        try
        {
            return new NamespacedKey(Key);
        }
        catch (ArgumentException)
        {
            throw new PackContentException($"Invalid component key: \"{Key}\"");
        }
    }



    // Methods.
    public EntityComponent GetComponent(JSONCompound compound)
    {
        NamespacedKey Key = GetComponentKey(compound);

        if (_deconstructors.TryGetValue(Key, out var Deconstructor))
        {
            return Deconstructor.DeconstructComponent(compound, _genericDeconstructor);
        }

        throw new PackContentException($"Unknown component found: {Key}");
    }
}