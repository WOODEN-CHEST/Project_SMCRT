using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class SimulationSpeedChangeArgs : EventArgs
{
    // Fields.
    public double NewSimulationSpeed { get; private init; }


    // Constructors.
    public SimulationSpeedChangeArgs(double newSimulationSpeed)
    {
        NewSimulationSpeed = newSimulationSpeed;
    }
}