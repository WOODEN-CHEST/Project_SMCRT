using GHEngine;
using Project_SMCRT_Server.Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public interface IGameWorld : IEntityCollection, ITimeUpdatable
{
    // Fields.
    double SimulationSpeed { get; set; }
    bool IsSimulationRunning { get; set; }
    IDataPack UsedDataPack { get; set; }



    // Methods.
    void ScheduleAction(Action action);
}