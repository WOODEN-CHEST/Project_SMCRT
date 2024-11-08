using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class ResourceContainerDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_COMPOSITES = "composites";
    public const string KEY_COMPOSITES_PER_SECOND = "composites_per_second";
    public const string KEY_METAL = "metal";
    public const string KEY_METAL_PER_SECOND = "metal_per_second";
    public const string KEY_FUEL = "fuel";
    public const string KEY_FUEL_PER_SECOND = "fuel_per_second";
    public const string KEY_RESEARCH = "research";
    public const string KEY_RESEARCH_PER_SECOND = "research_per_second";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        ResourceContainerComponent Component = new();

        if (compound.GetOptionalVerified(KEY_COMPOSITES, out object? Composites))
        {
            Component.Composites = genericDeconstructor.GetAsDouble(Composites!);
        }
        if (compound.GetOptionalVerified(KEY_COMPOSITES_PER_SECOND, out object? CompositesPerSecond))
        {
            Component.CompositesPerSecond = genericDeconstructor.GetAsDouble(CompositesPerSecond!);
        }
        if (compound.GetOptionalVerified(KEY_METAL, out object? Metal))
        {
            Component.Metal = genericDeconstructor.GetAsDouble(Metal!);
        }
        if (compound.GetOptionalVerified(KEY_METAL_PER_SECOND, out object? MetalPerSecond))
        {
            Component.MetalPerSecond = genericDeconstructor.GetAsDouble(MetalPerSecond!);
        }
        if (compound.GetOptionalVerified(KEY_FUEL, out object? Fuel))
        {
            Component.Fuel = genericDeconstructor.GetAsDouble(Fuel!);
        }
        if (compound.GetOptionalVerified(KEY_FUEL_PER_SECOND, out object? FuelPerSecond))
        {
            Component.FuelPerSecond = genericDeconstructor.GetAsDouble(FuelPerSecond!);
        }
        if (compound.GetOptionalVerified(KEY_RESEARCH, out object? Research))
        {
            Component.Research = genericDeconstructor.GetAsDouble(Research!);
        }
        if (compound.GetOptionalVerified(KEY_RESEARCH_PER_SECOND, out object? ResearchPerSecond))
        {
            Component.ResearchPerSecond = genericDeconstructor.GetAsDouble(ResearchPerSecond!);
        }

        return Component;
    }
}