using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class PlayerComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("player");


    // Fields.
    public ulong PlayerID { get; private init; }


    // Constructors.
    public PlayerComponent(ulong playerID) : base(KEY)
    {
        PlayerID = playerID;
    }


    // Inherited methods.
    public override EntityComponent CreateCopy()
    {
        return new PlayerComponent(PlayerID);
    }
}