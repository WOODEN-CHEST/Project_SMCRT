using GHEngine;
using GHEngine.Assets;
using GHEngine.Assets.Def;
using GHEngine.Assets.Loader;
using GHEngine.Audio;
using GHEngine.Frame;
using GHEngine.Frame.Animation;
using GHEngine.IO;
using GHEngine.Logging;
using GHEngine.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_SMCRT_Client.Section.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Project_SMCRT_Client.Section;

public class SMCRTGame : Game, ISectionHolder
{
    // Fields.
    public GameSection CurrentGameSection
    {
        get => _upcomingSection ?? _currentSection;
        set => _upcomingSection = value ?? throw new ArgumentNullException(nameof(value));
    }

    // Private static fields.
    private const string GAME_NAME = "Project S.M.C.R.T";
    private const string GAME_NAME_INTERNAL = "project_smcrt";

    private const string DIR_LOGS = "logs";
    private const string DIR_LOGS_OLD = "old";
    private const string FILE_LOGS_LATEST = "latest.log";
    private const string DIR_ASSETS = "assets";
    private const string DIR_DEFINITIONS = "definitions";


    // Private fields.
    private readonly GraphicsDeviceManager _graphicsManager;
    private string _rootPath;
    private string _rootApplicationDataPath;
    private string _logPath;
    private ILogger _logger;
    private IFrameRenderer _frameRender;
    private GameSection _currentSection;
    private GameSection? _upcomingSection;
    private IAssetStreamOpener _assetStreamOpener;
    private IAssetProvider _assetProvider;
    private IDisplay _display;
    private IAudioEngine _audioEngine;
    private IUserInput _userInput;
    private readonly IAssetDefinitionCollection _assetDefinitions = new GHAssetDefinitionCollection();
    private readonly IModifiableProgramTime _time = new GenericProgramTime();
    private GameServices _services;


    // Constructors.
    public SMCRTGame()
    {
        _graphicsManager = new(this);
    }


    // Private methods.
    private void OnCrash(Exception e)
    {
        _logger.Critical($"Game has crashed! Exception message: {e}");
        ShutDownComponents();
        Process.Start("notepad", _logPath);
        Exit();
    }

    private void InitializeLogger()
    {
        string LogDir = Path.Combine(_rootApplicationDataPath, DIR_LOGS);
        Directory.CreateDirectory(LogDir);
        _logPath = Path.Combine(LogDir, FILE_LOGS_LATEST);
        
        if (File.Exists(_logPath))
        {
            string OldLogDir = Path.Combine(LogDir, DIR_LOGS_OLD);
            Directory.CreateDirectory(OldLogDir);
            new GHLogArchiver().Archive(OldLogDir, _logPath);
        }

        File.Delete(_logPath);
        _logger = new GHLogger(_logPath);
    }

    private void InitializeAssetProvider()
    {
        string RootAssetPath = Path.Combine(_rootPath, DIR_ASSETS);
        Directory.CreateDirectory(RootAssetPath);

        _assetStreamOpener = new GHAssetStreamOpener(RootAssetPath);
        GHGenericAssetLoader GenericLoader = new GHGenericAssetLoader();

        GenericLoader.SetTypeLoader(AssetType.Animation, new AnimationLoader(_assetStreamOpener, GraphicsDevice));
        GenericLoader.SetTypeLoader(AssetType.Font, new FontLoader(_assetStreamOpener, GraphicsDevice));
        GenericLoader.SetTypeLoader(AssetType.Sound, new SoundLoader(_assetStreamOpener, _audioEngine.WaveFormat));
        GenericLoader.SetTypeLoader(AssetType.Shader, new ShaderLoader(Content));

        GHAssetProvider AssetProvider = new GHAssetProvider(GenericLoader, _assetDefinitions, _logger);
        InitializeDefaultAssets(AssetProvider);
        _assetProvider = AssetProvider;

        string DefinitionPath = Path.Combine(RootAssetPath, DIR_DEFINITIONS);
        JSONAssetDefinitionReader AssetDefReader = new(_logger);
        AssetDefReader.Read(_assetDefinitions, DefinitionPath);
    }

    private void InitializeDefaultAssets(GHAssetProvider assetProvider)
    {
        Texture2D DefaultTexture = new(GraphicsDevice, 2, 2, false, SurfaceFormat.Color);
        DefaultTexture.SetData(new Color[] { Color.Black, Color.Purple, Color.Purple, Color.Black });
        ISpriteAnimation Animation = new GHSpriteAnimation(0d, 0, false, null, false, DefaultTexture);
        assetProvider.SetDefaultAsset(AssetType.Animation, Animation);
    }

    private void ShutDownComponents()
    {
        _audioEngine?.Stop();
        _audioEngine?.Dispose();
        _frameRender?.Dispose();
        _display?.Dispose();
        _assetProvider?.ReleaseAllAssets();
        _logger?.Dispose();
    }


    // Inherited methods.
    protected override void LoadContent()
    {
        try
        {
            _rootPath = Path.GetDirectoryName((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location)!;
            _rootApplicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GAME_NAME_INTERNAL);
            Directory.CreateDirectory(_rootApplicationDataPath);

            _audioEngine = new GHAudioEngine(15);
            _audioEngine.Start();

            InitializeLogger();
            InitializeAssetProvider();

            _display = new GHDisplay(_graphicsManager, Window);
            _frameRender = new GHRenderer(GraphicsDevice, _display);

            _display.Initialize();
            _frameRender.Initialize();    

            _userInput = new GHUserInput(Window, this);
            _display.ScreenSizeChange += ((sender, args) => _userInput.InputAreaSizePixels = (Vector2)args.NewSize);
            _userInput.InputAreaSizePixels = (Vector2)_display.CurrentWindowSize;

            _display.IsUserResizingAllowed = true;
            _display.FullScreenSize = _display.ScreenSize;

            _logger.Info("Started game.");
            
            Window.Title = GAME_NAME;
            _userInput.IsMouseVisible = true;

            _services = new(_logger, _audioEngine, _display, this, _userInput);

            _currentSection = new MainMenuSection(_services);
            _currentSection.Load(_assetProvider);
            _currentSection.Start();
        }
        catch (Exception e)
        {
            OnCrash(e);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        try
        {
            _frameRender.RenderFrame(_currentSection.Frame, _time);
        }
        catch (Exception e)
        {
            OnCrash(e);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _time.TotalTime += gameTime.ElapsedGameTime;
        _time.PassedTime = gameTime.ElapsedGameTime;

        try
        {
            if (_upcomingSection != null)
            {
                _currentSection.End();
                _upcomingSection.Start();
                Task.Run(() => _currentSection.Unload(_assetProvider));
                _currentSection = _upcomingSection;
                _upcomingSection = null;
            }

            _userInput.RefreshInput();
            if (_userInput.WereKeysJustPressed(Keys.F11))
            {
                _display.IsFullScreen = !_display.IsFullScreen;
            }

            _currentSection.Update(_time);
        }
        catch (Exception e)
        {
            OnCrash(e);
        }
    }

    protected override void EndRun()
    {
        base.EndRun();
        try
        {
            ShutDownComponents();
        }
        catch (Exception e)
        {
            OnCrash(e);
        }
    }

    public void CloseAllSections()
    {
        _currentSection.End();
        Exit();
    }
}