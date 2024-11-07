using GHEngine.Audio;
using GHEngine.IO;
using GHEngine.Logging;
using GHEngine.Screen;
using Project_SMCRT_Client.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Client;

public class GameServices
{
    // Fields.
    public ILogger? Logger { get; private init; }
    public IAudioEngine AudioEngine { get; private init; }
    public IDisplay Display { get; private init; }
    public ISectionHolder SectionHolder { get; private init; }
    public IUserInput UserInput { get; private init; }


    // Constructors.
    public GameServices(ILogger? logger,
        IAudioEngine audioEngine,
        IDisplay display,
        ISectionHolder sectionHolder,
        IUserInput userInput)
    {
        Logger = logger;
        AudioEngine = audioEngine ?? throw new ArgumentNullException(nameof(audioEngine));
        Display = display ?? throw new ArgumentNullException(nameof(display));
        SectionHolder = sectionHolder ?? throw new ArgumentNullException(nameof(SectionHolder));
        UserInput = userInput ?? throw new ArgumentNullException(nameof(UserInput));
    }
}