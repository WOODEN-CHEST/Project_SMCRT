using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class EnemyDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_AGGRESSIVENESS = "aggressiveness";
    public const string KEY_COOPERATION = "cooperation";
    public const string KEY_REACTION_SPEED = "reaction_speed";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        EnemyComponent Component = new();

        
        if (compound.GetOptionalVerified(KEY_AGGRESSIVENESS, out object? Aggressiveness))
        {
            Component.Aggressiveness = genericDeconstructor.GetAsDouble(Aggressiveness!);
        }
        if (compound.GetOptionalVerified(KEY_COOPERATION, out object? Cooperation))
        {
            Component.Aggressiveness = genericDeconstructor.GetAsDouble(Cooperation!);
        }
        if (compound.GetOptionalVerified(KEY_REACTION_SPEED, out object? ReactionSpeed))
        {
            Component.Aggressiveness = genericDeconstructor.GetAsDouble(ReactionSpeed!);
        }

        return Component;
    }
}