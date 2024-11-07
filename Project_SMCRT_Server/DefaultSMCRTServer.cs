using GHEngine;
using GHEngine.Collections;
using GHEngine.Logging;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.Pack.JSON;
using Project_SMCRT_Server.Packet;
using Project_SMCRT_Server.Packet.Server;
using Project_SMCRT_Server.Player;
using Project_SMCRT_Server.World;
using Project_SMCRT_Server.World.Monitor;
using Project_SMCRT_Server.World.Script;
using System.Diagnostics;

namespace Project_SMCRT_Server;

public class DefaultSMCRTServer : ISMCRTServer
{
    // Static fields.
    public const double DEFAULT_TIME_BETWEEN_PACKETS_SECONDS = 0.05d;
    public const string DIR_DATAPACKS = "datapacks";


    // Fields.
    public Version ServerVersion { get; } = new(1, 0, 0, 0); 
    public ILogger? Logger { get; private init; }
    public bool IsInternalServer { get; private init; }
    public IDataPack Pack { get; private set; } = new DefaultDataPack();
    public IEnumerable<IGameWorld> Worlds => _worlds.Values.Select(bundle => bundle.World);
    public double SimulationSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            if (!value)
            {
                _unpauseEvent.Set();
            }
        }
    }


    // Private fields.
    private readonly IPlayerManager _playerManager;
    private readonly Dictionary<ulong, GameWorldBundle> _worlds = new();
    private readonly DiscreteTimeCollection<Action> _scheduledActions = new();
    private readonly string _rootPath;
    private readonly ServerPacketCreator _packetCreator = new();

    private bool _isRunning = false;
    private bool _isPaused = false;
    private readonly AutoResetEvent _unpauseEvent = new(false);

    private TimeSpan _timeBetweenPacketSends = TimeSpan.FromSeconds(DEFAULT_TIME_BETWEEN_PACKETS_SECONDS);
    private TimeSpan _timeSinceLastPacket = TimeSpan.Zero;


    // Constructors.
    public DefaultSMCRTServer(SMCRTServerOptions options,
        string rootPath,
        ILogger? logger, 
        IPacketReceiver? receiver, 
        bool isInternalServer)
    {
        Logger = logger == null ? null : new ServerLogger(logger);
        IsInternalServer = isInternalServer;
        _rootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
        IClientPacketProcessor PacketProcessor = new DefaultClientPacketProcessor(this);
        _playerManager = isInternalServer ? new IntegratedPlayerManager(receiver!, PacketProcessor)
            : new StandalonePlayerManager(PacketProcessor, options.ServerAddress!, options.ServerPort!.Value, Logger);
    }


    // Private methods.
    private void ExecuteScheduledActions()
    {
        lock (_scheduledActions)
        {
            _scheduledActions.ApplyChanges();
        }
        foreach (Action SchedulesActon in _scheduledActions)
        {
            SchedulesActon.Invoke();
        }
        _scheduledActions.Clear();
    }

    private void InitializeWorlds()
    {
        _worlds.Clear();

        IGameWorld CreatedWorld = new DefaultGameWorld(0uL, Pack);

        _worlds.Add(1uL, new(CreatedWorld, new DefaultWorldMonitor(CreatedWorld)));
        Logger?.Info("Created new world");
    }

    private void ExecuteMainLoop()
    {
        Stopwatch TimeMeasurer = new();
        IModifiableProgramTime Time = new GenericProgramTime();

        while (_isRunning)
        {
            while (_isPaused)
            {
                _unpauseEvent.WaitOne();
                ExecuteScheduledActions();
            }

            TimeMeasurer.Reset();
            TimeMeasurer.Start();

            ExecuteScheduledActions();
            
            foreach (GameWorldBundle Bundle in _worlds.Values)
            {
                Bundle.World.Update(Time);
            }

            UpdatePackets(Time);

            TimeMeasurer.Stop();
            Time.PassedTime = TimeMeasurer.Elapsed;
            Time.TotalTime += TimeMeasurer.Elapsed;
        }
    }

    private void UpdatePackets(IProgramTime time)
    {
        _timeSinceLastPacket += time.PassedTime;
        if (_timeSinceLastPacket < _timeBetweenPacketSends)
        {
            return;
        }
        _timeSinceLastPacket = TimeSpan.Zero;

        foreach (GameWorldBundle Bundle in _worlds.Values)
        {
            foreach (ServerPacket Packet in _packetCreator.GetPackets(Bundle.Monitor))
            {
                foreach (ulong PlayerID in _playerManager.Players)
                {
                    _playerManager.SendPacket(PlayerID, Packet);
                }
            }
        }
    }


    // Inherited methods.
    public void Run()
    {
        try
        {
            Logger?.Info("Initializing server");
            Pack = new JSONPackParser().ParseCombinedPack(Path.Combine(_rootPath, DIR_DATAPACKS));
            Logger?.Info("Parsed all data-packs");

            _isRunning = true;
            
            _playerManager.Start();
            InitializeWorlds();

            Logger?.Info($"Started running Project S.M.C.R.T " +
                $"server version {ServerVersion} ({(IsInternalServer ? "Integrated" : "Standalone")})");
            ExecuteMainLoop();

            Logger?.Info($"Stopping server");
            _playerManager.End();

            Logger?.Info($"Server stopped");
        }
        catch (PackContentException e)
        {
            Logger?.Error($"Invalid DataPack content, cannot run server: {e.Message}");
        }
        catch (Exception e)
        {
            Logger?.Critical($"Server has crashed! Exception: {e}");
        }
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void ScheduleAction(Action action)
    {
        lock (_scheduledActions)
        {
            _scheduledActions.Add(action);
            _unpauseEvent.Set();
        }
    }

    public IGameWorld? GetWorld(ulong id)
    {
        _worlds.TryGetValue(id, out var WorldBundle);
        return WorldBundle?.World;
    }

    public void ReceivePacket(SMCRTPacket packet)
    {
        _playerManager.ReceivePacket(packet);
    }

    public void RunWorld(IWorldScript script)
    {
        ulong ID = 1uL;
        IGameWorld World = new DefaultGameWorld(ID, Pack);
        _worlds.Add(ID, new GameWorldBundle(World, new DefaultWorldMonitor(World)));
    }


    // Types.
    private record class GameWorldBundle(IGameWorld World, IWorldMonitor Monitor);
}