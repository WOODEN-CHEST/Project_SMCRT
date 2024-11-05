using GHEngine;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public interface IGameWorld : IEntityCollection, ITimeUpdatable
{
    // Fields.
    ulong ID { get; }
    double SimulationSpeed { get; set; }
    bool IsSimulationPaused { get; set; }
    IDataPack UsedDataPack { get; }
    ulong Planet { get; }

    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;
    public event EventHandler<SimulationSpeedChangeArgs> SimulationSpeedChange;
    public event EventHandler<PauseChangeArgs> PauseChange;



    // Methods.
    void ScheduleAction(Action action);
}