using GHEngine;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World.Component;
using Project_SMCRT_Server.World.Script;
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
    IWorldScript Script { get; set; }
    public string? CurrentMusicName { get; set; }

    public event EventHandler<ComponentUpdateEventArgs>? ComponentUpdate;
    public event EventHandler<SimulationSpeedChangeArgs> SimulationSpeedChange;
    public event EventHandler<PauseChangeArgs> PauseChange;
    public event EventHandler<WorldSoundPlayEventArgs> SoundPlay;



    // Methods.
    void ScheduleAction(Action action);
    void PlaySound(string soundName, DVector2 position, double speed, float volume, int? sampleRate);
}