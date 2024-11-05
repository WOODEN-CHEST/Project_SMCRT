using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class PhysicalPropertiesComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("physical_properties");

    public const double MASS_DEFAULT = 1d;
    public const double MASS_MIN = double.Epsilon;
    public const double MASS_MAX = 1e18d;

    public const double TEMPERATURE_DEFAULT = 273.15;
    public const double TEMPERATURE_MIN = double.Epsilon;
    public const double TEMPERATURE_MAX = double.MaxValue;

    public const double ATTRACTION_DEFAULT = 0d;
    public const double ATTRACTION_MIN = -100_000d;
    public const double ATTRACTION_MAX = 100_000d;

    public const double HEALTH_DEFAULT = 100d;
    public const double HEALTH_MIN = 0d;
    public const double HEALTH_MAX = 1e18d;

    public const double DEFAULT_FLAME_START_TEMPERATURE = 3 * TEMPERATURE_DEFAULT;

    public const double FIRE_RESISTANCE_DEFAULT = 0.2d;
    public const double FIRE_RESISTANCE_MIN = 0d;
    public const double FIRE_RESISTANCE_MAX = 1d;

    public const double HEAT_CAPACITY_DEFAULT = 1d;
    public const double HEAT_CAPACITY_MIN = double.Epsilon;
    public const double HEAT_CAPACITY_MAX = 1e18;


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

    public double Attraction
    {
        get => _attraction;
        set => _attraction = double.IsNaN(value) ? ATTRACTION_DEFAULT
            : Math.Clamp(value, ATTRACTION_MIN, ATTRACTION_MAX);
    }

    public double Health
    {
        get => _health;
        set => _health = double.IsNaN(value) ? HEALTH_DEFAULT
            : Math.Clamp(value, HEALTH_MIN, HEALTH_MAX);
    }

    public double FlameStartTemperature
    {
        get => _flameStartTemperature;
        set => _flameStartTemperature = double.IsNaN(value) ? DEFAULT_FLAME_START_TEMPERATURE
            : Math.Clamp(value, TEMPERATURE_MIN, TEMPERATURE_MAX);
    }

    public double FireResistance
    {
        get => _fireResistance;
        set => _fireResistance = double.IsNaN(value) ? FIRE_RESISTANCE_DEFAULT
            : Math.Clamp(value, FIRE_RESISTANCE_MIN, FIRE_RESISTANCE_MAX);
    }

    public double HeatCapacity
    {
        get => _heatCapacity;
        set => _heatCapacity = double.IsNaN(value) ? HEAT_CAPACITY_DEFAULT
            : Math.Clamp(value, HEAT_CAPACITY_MIN, HEAT_CAPACITY_MAX);
    }

    public bool IsOnFire { get; set; }


    // Private fields.
    private double _mass = MASS_DEFAULT;
    private double _health = HEALTH_DEFAULT;
    private double _temperature = TEMPERATURE_DEFAULT;
    private double _attraction = ATTRACTION_DEFAULT;
    private double _flameStartTemperature = ATTRACTION_DEFAULT;
    private double _fireResistance = FIRE_RESISTANCE_DEFAULT;
    private double _heatCapacity = HEAT_CAPACITY_DEFAULT;


    // Constructors.
    public PhysicalPropertiesComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new PhysicalPropertiesComponent()
        {
            Mass = Mass,
            Temperature = Temperature,
            Attraction = Attraction,
        };
    }
}