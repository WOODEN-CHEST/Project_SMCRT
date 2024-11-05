using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public readonly struct InputAction
{
    // Static fields.
    public static InputAction None => new(-1);


    // Fields.
    public int Action { get; init; }


    // Constructors.
   public InputAction(int action)
    {
        Action = action;
    }
}