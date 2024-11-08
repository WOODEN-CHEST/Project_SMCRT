using GHEngine.IO.JSON;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack.JSON;

public class PhysicalPropertiesDeconstructor : IJSONComponentDeconstructor
{
    // Static fields.
    public const string KEY_HEALTH = "health";
    public const string KEY_MAX_HEALTH = "max_health";
    public const string KEY_ATTRACTION = "attraction";
    public const string KEY_HEAT_CAPACITY = "heat_capacity";
    public const string KEY_FLAME_START_TEMPERATURE = "flame_start_temperature";
    public const string KEY_TEMPERATURE = "temperature";
    public const string KEY_FIRE_RESISTANCE = "fire_resistance";
    public const string KEY_MASS = "mass";


    // Inherited methods.
    public EntityComponent DeconstructComponent(JSONCompound compound, GenericJSONDeconstructor genericDeconstructor)
    {
        PhysicalPropertiesComponent Component = new();

        if (compound.GetOptionalVerified(KEY_MAX_HEALTH, out object? MaxHealth))
        {
            Component.MaxHealth = genericDeconstructor.GetAsDouble(MaxHealth!);
        }
        if (compound.GetOptionalVerified(KEY_HEALTH, out object? Health))
        {
            Component.Health = genericDeconstructor.GetAsDouble(Health!);
        }
        if (compound.GetOptionalVerified(KEY_ATTRACTION, out object? Attriation))
        {
            Component.Attraction = genericDeconstructor.GetAsDouble(Attriation!);
        }
        if (compound.GetOptionalVerified(KEY_HEAT_CAPACITY, out object? HeatCapacity))
        {
            Component.HeatCapacity = genericDeconstructor.GetAsDouble(HeatCapacity!);
        }
        if (compound.GetOptionalVerified(KEY_FLAME_START_TEMPERATURE, out object? FlameStartTemperature))
        {
            Component.FlameStartTemperature = genericDeconstructor.GetAsDouble(FlameStartTemperature!);
        }
        if (compound.GetOptionalVerified(KEY_TEMPERATURE, out object? Temperature))
        {
            Component.Temperature = genericDeconstructor.GetAsDouble(Temperature!);
        }
        if (compound.GetOptionalVerified(KEY_FIRE_RESISTANCE, out object? FireResistance))
        {
            Component.FireResistance = genericDeconstructor.GetAsDouble(FireResistance!);
        }
        if (compound.GetOptionalVerified(KEY_MASS, out object? Mass))
        {
            Component.Mass = genericDeconstructor.GetAsDouble(Mass!);
        }

        return Component;
    }
}