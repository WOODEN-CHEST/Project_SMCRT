using GHEngine;
using GHEngine.Assets;
using GHEngine.Assets.Def;
using GHEngine.Audio;
using GHEngine.Audio.Source;
using GHEngine.Frame;
using GHEngine.Frame.Animation;
using GHEngine.GameFont;
using Microsoft.Xna.Framework.Audio;
using Project_SMCRT_Client.Section.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_SMCRT_Client.Section.Menu;

public class MainMenuSection : GameSection
{
    // Fields.
    public override IGameFrame Frame { get; } = new GHGameFrame();


    // Private fields.
    private readonly string BACKGROUND_LAYER_NAME = "background";
    private readonly string FOREGROUND_LAYER_NAME = "foreground";

    private readonly GameServices _services;

    private GameBackground _background;
    private IPreSampledSoundInstance _music;

    private GenericButton[] _buttons;


    // Constructors.
    public MainMenuSection(GameServices services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }

    // Private methods.
    private void InitializeMainButtons(IAssetProvider assetProvider)
    {
        IAnimationInstance ButtonSprite = assetProvider.GetAsset<ISpriteAnimation>
            (this, AssetType.Animation, "UI.GenericButton")!.CreateInstance();
        ISound HighlightSound = assetProvider.GetAsset<ISound>(this, AssetType.Sound, "UI.Highlight")!;
        ISound[] ClickSounds = new ISound[]
        {
            assetProvider.GetAsset<IPreSampledSound>(this, AssetType.Sound, "UI.Click1")!,
            assetProvider.GetAsset<IPreSampledSound>(this, AssetType.Sound, "UI.Click2")!,
            assetProvider.GetAsset<IPreSampledSound>(this, AssetType.Sound, "UI.Click3")!,
            assetProvider.GetAsset<IPreSampledSound>(this, AssetType.Sound, "UI.Click4")!
        };
        GHFontFamily Font = assetProvider.GetAsset<GHFontFamily>(this, AssetType.Font, "Default")!;

        _buttons = new GenericButton[]
        {
            new GenericButton("Singleplayer", Font, ButtonSprite, HighlightSound, ClickSounds, _services)
            {
                Position = new(0.5f, 0.45f),
                ButtonColor = GenericButton.ColorGenericBlue
            },
            new GenericButton("Multiplayer", Font, ButtonSprite, HighlightSound, ClickSounds, _services)
            {
                Position = new(0.5f, 0.6f),
                ButtonColor = GenericButton.ColorGenericPurple
            },
            new GenericButton("Options", Font, ButtonSprite, HighlightSound, ClickSounds, _services)
            {
                Position = new(0.5f, 0.75f),
                ButtonColor = GenericButton.ColorGenericPink
            },
            new GenericButton("Exit", Font, ButtonSprite, HighlightSound, ClickSounds, _services)
            {
                Position = new(0.5f, 0.9f),
                ButtonColor = GenericButton.ColorBad,
                LeftClickAction = () => _services.SectionHolder.CloseAllSections()
            },
        };

        ILayer Foreground = Frame.GetLayer(FOREGROUND_LAYER_NAME)!;
        foreach (GenericButton button in _buttons)
        {
            Foreground.AddItem(button);
        }
    }

    private void InitializeBackground(IAssetProvider assetProvider)
    {
        _background = new(assetProvider.GetAsset<ISpriteAnimation>(this, AssetType.Animation, "SpaceBackground")!.CreateInstance());
        Frame.GetLayer(BACKGROUND_LAYER_NAME)!.AddItem(_background);
    }

    private void InitializeLayers()
    {
        ILayer BackgroundLayer = new GHLayer("background");
        ILayer ForeGroundLayer = new GHLayer("foreground");
        Frame.AddLayer(BackgroundLayer);
        Frame.AddLayer(ForeGroundLayer);
    }

    private void InitializeMusic(IAssetProvider assetProvider)
    {
        _music = (GHPreSampledSoundInstance)assetProvider.GetAsset<IPreSampledSound>
            (this, AssetType.Sound, "MainMenu.Music")!.CreateInstance();
        _music.IsLooped = true;
    }



    // Inherited methods,
    public override void Load(IAssetProvider assetProvider)
    {
        InitializeLayers();
        InitializeMusic(assetProvider);
        InitializeMainButtons(assetProvider);
        InitializeBackground(assetProvider);
    }

    public override void Unload(IAssetProvider assetProvider)
    {
        assetProvider.ReleaseUserAssets(this);
    }

    public override void Update(IProgramTime time)
    {
        foreach (GenericButton button in _buttons)
        {
            button.Update(time);
        }
    }

    public override void Start()
    {
        _services.AudioEngine.ScheduleAction(() => _services.AudioEngine.AddSoundInstance(_music));
    }

    public override void End()
    {
        _services.AudioEngine.ScheduleAction(() => _services.AudioEngine.RemoveSoundInstance(_music));
    }
}