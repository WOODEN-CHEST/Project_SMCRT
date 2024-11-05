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


    // Fields.
    public double Damage { get; set; }


    // Constructors.
    public DamageComponent() : base(KEY)
    {
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new DamageComponent()
        {
            Damage = Damage,
        };
    }
}