using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class DamageCeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_DAMAGE = "damage";
    public const string KEY_DAMAGE_SPEED_SCALE = "speed_scale";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        DamageComponent Component = new();

        
        if (compound.GetOptionalVerified(KEY_DAMAGE, out object? Damage))
        {
            Component.Damage = genericDeconstructor.GetAsDouble(Damage);
        }
        if (compound.GetOptionalVerified(KEY_DAMAGE_SPEED_SCALE, out object? SpeecScale))
        {
            Component.SpeedScale = genericDeconstructor.GetAsDouble(SpeecScale);
        }

        return Component;
    }
}