using GHEngine.Logging;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public interface ISMCRTServer
{
    // Fields.
    Version ServerVersion { get; }
    ILogger? Logger { get; }
    bool IsInternalServer { get; }
    IDataPack CombinedDataPack { get; }
    IEnumerable<IGameWorld> Worlds { get; }
    double SimulationSpeed { get; set; }
    bool IsPaused { get; set; }



    // Methods.
    void Run();
    void Stop();
    void ScheduleAction(Action action);
}