using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Monitor;

public interface IWorldMonitor
{
    // Fields.
    public IGameWorld MonitoredWorld { get; }
    IEnumerable<AddedEntity> EntitiesAdded { get; }
    IEnumerable<RemovedEntity> EntitiesRemoved { get; }
    IEnumerable<AddedComponent> ComponentsAdded { get; }
    IEnumerable<RemovedComponent> ComponentsRemoved { get; }
    IEnumerable<UpdatedComponent> ComponentsUpdated { get; }
    double? NewSimulationSpeed { get; }
    bool? IsSimulationPaused { get; }



    // Methods.
    void StartMonitor();
    void EndMonitor();
    void ResetMonitors();
}