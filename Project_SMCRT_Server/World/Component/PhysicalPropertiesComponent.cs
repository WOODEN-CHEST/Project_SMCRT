using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class PhysicalPropertiesComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = new(NamespacedKey.NAMESPACE_SMCRT, "physical_properties");

    public const double MASS_DEFAULT = 1d;
    public const double MASS_MIN = double.Epsilon;
    public const double MASS_MAX = 1e18d;

    public const double TEMPERATURE_DEFAULT = 273.15;
    public const double TEMPERATURE_MIN = double.Epsilon;
    public const double TEMPERATURE_MAX = double.MaxValue;


    // Fields.
    public double Mass
    {
        get => _mass;
        set => _mass = double.IsNaN(value) ? MASS_DEFAULT
            : Math.Clamp(value, MASS_MIN, MASS_MAX);
    }

    public double Temperature
    {
        get => _temperature;
        set => _temperature = double.IsNaN(value) ? TEMPERATURE_DEFAULT 
            : Math.Clamp(value, TEMPERATURE_MIN, TEMPERATURE_MAX);
    }


    // Private fields.
    private double _mass = MASS_DEFAULT;
    private double _temperature = TEMPERATURE_DEFAULT;


    // Constructors.
    public PhysicalPropertiesComponent(NamespacedKey key) : base(key)
    {
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        throw new NotImplementedException();
    }
}