using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHEngine;

namespace Project_SMCRT_Server.World;

public class WorldTime : ITimeUpdatable
{
    // Static fields.
    public const double SIMULATION_SPEED_DEFAULT = 1d;
    public const double SIMULATION_SPEED_MIN = 0d;
    public const double SIMULATION_SPEED_MAX = 10_000d;


    // Fields
    public IModifiableProgramTime VirtualTime { get; private init; } = new GenericProgramTime();
    public IModifiableProgramTime RealTime { get; private init; } = new GenericProgramTime();
    public double SimulationSpeed
    {
        get => _simulationSpeed;
        set => _simulationSpeed = double.IsNaN(value) ? SIMULATION_SPEED_DEFAULT 
            : Math.Clamp(value, SIMULATION_SPEED_MIN, SIMULATION_SPEED_MAX);
    }


    // Private fields.
    private double _simulationSpeed = 1d;


    // Methods.
    public void Update(IProgramTime time)
    {
        RealTime.TotalTime = time.TotalTime;
        RealTime.PassedTime = time.PassedTime;

        TimeSpan PassedTime = time.PassedTime * SimulationSpeed;
        VirtualTime.PassedTime = PassedTime;
        VirtualTime.TotalTime += PassedTime;
    }
}