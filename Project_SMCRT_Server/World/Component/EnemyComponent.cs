using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class EnemyComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("enemy");

    public const double AGGRESSIVENESS_DEFAULT = 0.4d;
    public const double AGGRESSIVENESS_MIN = 0d;
    public const double AGGRESSIVENESS_MAX = 1d;

    public const double COOPERATION_DEFAULT = 0.2d;
    public const double COOPERATION_MIN = 0d;
    public const double COOPERATION_MAX = 1d;

    public readonly TimeSpan REACTION_SPEED_DEFAULT = TimeSpan.FromSeconds(0.2d);
    public readonly TimeSpan REACTION_SPEED_MIN = TimeSpan.Zero;
    public readonly TimeSpan REACTION_SPEED_MAX = TimeSpan.FromSeconds(60d);


    // Fields.
    public double Aggressiveness
    {
        get => _aggressiveness;
        set
        {
            _aggressiveness = double.IsNaN(value) ? AGGRESSIVENESS_DEFAULT :
                Math.Clamp(value, AGGRESSIVENESS_MIN, AGGRESSIVENESS_MAX);
        }
    }

    public double Cooperation
    {
        get => _cooperation;
        set
        {
            _cooperation = double.IsNaN(value) ? COOPERATION_DEFAULT :
                Math.Clamp(value, COOPERATION_MIN, COOPERATION_MAX);
        }
    }

    public TimeSpan ReactionSpeed
    {
        get => _reactionSpeed;
        set
        {
            _reactionSpeed = TimeSpan.FromSeconds(Math.Clamp(value.TotalSeconds, 
                REACTION_SPEED_MIN.TotalSeconds, REACTION_SPEED_MAX.TotalSeconds));
        }
    }

    // Private fields.
    private double _aggressiveness;
    private double _cooperation;
    private TimeSpan _reactionSpeed;


    // Constructors.
    public EnemyComponent() : base(KEY) { }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        EnemyComponent CreatedComponent = new();
        CreatedComponent.SetFrom(this);
        return CreatedComponent;
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not EnemyComponent Target)
        {
            return false;
        }

        Aggressiveness = Target.Aggressiveness;
        Cooperation = Target.Cooperation;
        ReactionSpeed = Target.ReactionSpeed;

        return true;
    }
}