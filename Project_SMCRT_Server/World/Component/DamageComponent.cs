using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class DamageComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("damage");

    public const double DAMAGE_DEFAULT = 0d;
    public const double DAMAGE_MIN = -1e18;
    public const double DAMAGE_MAX = 1e18;


    public const double SPEED_DAMAGE_SCALE_DEFAULT = 1d;
    public const double SPEED_DAMAGE_SCALE_MIN = -1e18d;
    public const double SPEED_DAMAGE_SCALE_MAX = 1e18;


    // Fields.
    public double Damage
    {
        get => _damage;
        set => _damage = double.IsNaN(value) ? DAMAGE_DEFAULT
            : Math.Clamp(value, DAMAGE_MIN, DAMAGE_MAX);
    }

    public double SpeedDamageScale
    {
        get => _speedDamageScale;
        set => _speedDamageScale = double.IsNaN(value) ? SPEED_DAMAGE_SCALE_DEFAULT
            : Math.Clamp(value, SPEED_DAMAGE_SCALE_MIN, SPEED_DAMAGE_SCALE_MAX);
    }


    // Private fields.
    private double _damage;
    private double _speedDamageScale;


    // Constructors.
    public DamageComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new DamageComponent()
        {
            Damage = Damage,
            SpeedDamageScale = SpeedDamageScale,
        };
    }
}