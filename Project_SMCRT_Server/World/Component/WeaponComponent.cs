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
    public TimeSpan ReloadProgress { get; set; }
    public int AmmoLeft { get; set; }
    public InputAction RequiredInputAction { get; set; }


    // Constructors.
    public WeaponComponent() : base(KEY) { }



    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override bool SetFrom(EntityComponent component)
    {
        if (component is not  WeaponComponent Target)
        {
            return false;
        }

        WeaponKey = Target.WeaponKey;
        TimeSinceWeaponFire = Target.TimeSinceWeaponFire;
        ReloadProgress = Target.ReloadProgress;
        AmmoLeft = Target.AmmoLeft;
        RequiredInputAction = Target.RequiredInputAction;

        return true;
    }
}