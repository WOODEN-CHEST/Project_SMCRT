using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class ThrusterDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_THRUSTERS = "thrusters";
    public const string KEY_STRENGTH = "strength";
    public const string KEY_ROTATION = "rotation";
    public const string KEY_IS_ROTATION_FOLLOWED = "is_rotation_followed";
    public const string KEY_INPUT_KEY = "input_key";
    public const string KEY_OFFSET = "offset";


    // Private methods.
    private IEnumerable<EntityThruster> GetThrusters(JSONList thrusterList, GenericJSONDeconstructor genericDeconstructor)
    {
        List<EntityThruster> Thrusters = new();

        foreach (object? ListEntry in thrusterList)
        {
            if (ListEntry is not JSONCompound Compound)
            {
                throw new PackContentException("Expected compound for thruster entry.");
            }
            EntityThruster Thruster = new();

            if (Compound.GetOptionalVerified(KEY_STRENGTH, out object? Strength))
            {
                Thruster.Strength = genericDeconstructor.GetAsDouble(Strength!);
            }
            if (Compound.GetOptionalVerified(KEY_ROTATION, out object? Rotation))
            {
                Thruster.Rotation = genericDeconstructor.GetAsDouble(Rotation!);
            }
            if (Compound.GetOptionalVerified(KEY_IS_ROTATION_FOLLOWED, out bool IsRotationFollowed))
            {
                Thruster.FollowsRotation = IsRotationFollowed;
            }
            if (Compound.GetOptionalVerified(KEY_OFFSET, out JSONList? VertexList))
            {
                Thruster.Offset = genericDeconstructor.GetVector(VertexList!);
            }
        }

        return Thrusters;
    }


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        ThrusterComponent Component = new();

        if (compound.GetOptionalVerified(KEY_THRUSTERS, out JSONList? ThrusterList))
        {
            foreach (EntityThruster Thruster in GetThrusters(ThrusterList!, genericDeconstructor))
            {
                Component.AddThruster(Thruster);
            }
        }

        return Component;
    }
}