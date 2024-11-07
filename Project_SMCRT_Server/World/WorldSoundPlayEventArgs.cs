using GHEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World;

public class WorldSoundPlayEventArgs : EventArgs
{
    // Fields.
    public string SoundName { get; private init; }
    public double Speed { get; private init; }
    public float Volume { get; private init; }
    public int? SampleRate { get; private init; }
    public DVector2 Position { get; private init; }


    // Constructors.
    public WorldSoundPlayEventArgs(string soundName, DVector2 position, double speed, float volume, int? sampleRate)
    {
        SoundName = soundName ?? throw new ArgumentNullException(nameof(soundName));
        Speed = speed;
        Volume = volume;
        SampleRate = sampleRate;
        Position = position;
    }
}