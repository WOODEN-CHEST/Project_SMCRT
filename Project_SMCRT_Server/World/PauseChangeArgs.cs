using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class PauseChangeArgs : EventArgs
{
    // Fields.
    public bool IsPaused { get; private init; }


    // Constructors.
    public PauseChangeArgs(bool isPaused)
    {
        IsPaused = isPaused;
    }
}