using GHEngine;
using GHEngine.Collections;
using GHEngine.Logging;
using Project_SMCRT_Server.Pack;
using Project_SMCRT_Server.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public class DefaultSMCRTServer : ISMCRTServer
{
    // Fields.
    public Version ServerVersion { get; } = new(1, 0, 0, 0); 
    public ILogger? Logger { get; private init; }
    public bool IsInternalServer { get; private init; }
    public IDataPack CombinedDataPack { get; private set; } = new DefaultDataPack();
    public IEnumerable<IGameWorld> Worlds => _worlds;
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
    private readonly HashSet<IGameWorld> _worlds = new();
    private readonly DiscreteTimeCollection<Action> _scheduledActions = new();

    private bool _isRunning = false;
    private bool _isPaused = false;
    private readonly AutoResetEvent _unpauseEvent = new(false);


    // Constructors.
    public DefaultSMCRTServer(SMCRTServerOptions options, ILogger? logger = null, bool isInternalServer = false)
    {
        Logger = logger == null ? null : new ServerLogger(logger);
        IsInternalServer = isInternalServer;
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

        IGameWorld CreatedWorld = new DefaultGameWorld(CombinedDataPack);

        _worlds.Add(CreatedWorld);
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
            
            foreach (IGameWorld World in _worlds)
            {
                World.Update(Time);
            }

            TimeMeasurer.Stop();
            Time.PassedTime = TimeMeasurer.Elapsed;
            Time.TotalTime += TimeMeasurer.Elapsed;
        }
    }


    // Inherited methods.
    public void Run()
    {
        try
        {
            _isRunning = true;
            Logger?.Info($"Started Project S.M.C.R.T server version {ServerVersion} ({(IsInternalServer ? "Integrated" : "Standalone")})");

            InitializeWorlds();
            ExecuteMainLoop();

            Logger?.Info($"Stopped server");
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
}