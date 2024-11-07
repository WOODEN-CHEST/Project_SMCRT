using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class WeaponComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("weapon");


    // Fields.
    public NamespacedKey WeaponKey { get; set; }
    public TimeSpan TimeSinceWeaponFire { get; set; }


    // Constructors.
    public WeaponComponent() : base(KEY) { }



    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override bool SetFrom(EntityComponent component)
    {
        throw new NotImplementedException();
    }
}