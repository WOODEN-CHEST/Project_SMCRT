using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

public class WeaponDefinition
{
    public NamespacedKey Key { get; private init; }
    public TimeSpan DelayBetweenShots { get; private init; }
    public NamespacedKey EntityKey { get; private init; }
    public double SpreadAngle { get; private init; }
    public double AngleOffset { get; private init; }
    public double LaunchSpeedMin { get; private init; }
    public double LaucnSpeedMax { get; private init; }
    public EntityComponent[] EntityStartingComponents { get; private init; }
    public double ReloadTime { get; private init; }
    public int CartridgeSize { get; private init; }


    // Constructors.
    public WeaponDefinition(NamespacedKey key,
        TimeSpan delayBetweenShots,
        NamespacedKey entityKey,
        double spreadAngle,
        double angleOffset,
        double launchSpeedMin,
        double launchSpeedMax,
        EntityComponent[] startingComponents,
        double reloadTime,
        int cartridgeSize)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        DelayBetweenShots = delayBetweenShots;
        EntityKey = entityKey ?? throw new ArgumentNullException(nameof(key));
        SpreadAngle = spreadAngle;
        AngleOffset = angleOffset;
        LaunchSpeedMin = launchSpeedMin;
        LaucnSpeedMax = launchSpeedMax;
        EntityStartingComponents = startingComponents ?? throw new ArgumentNullException(nameof(launchSpeedMax));
        ReloadTime = reloadTime;
        CartridgeSize = cartridgeSize;
    }
}