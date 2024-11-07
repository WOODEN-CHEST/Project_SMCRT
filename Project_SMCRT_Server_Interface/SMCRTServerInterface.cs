using GHEngine.Logging;
using Project_SMCRT_Server_Interface;
using System.Net;
using System.Reflection;

namespace Project_SMCRT_Server;

internal class SMCRTServerInterface
{
    // Static fields.
    public const string DIR_LOGS = "logs";
    public const string DIR_LOGS_OLD = "old";
    public const string FILENAME_LOG = "latest.log";
    public const string FILENAME_CONFIG = "config.json";


    // Static methods.
    public static void Main(string[] args)
    {
        string RootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        ILogger Logger; 

        try
        {
            Logger = InitializeLogger(RootPath);
            Logger.Info("Initializing standalone SMCRT server.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to initialize logger, can't start server: {e}");
            return;
        }

        try
        {
            SMCRTServerOptions Configuration = LoadConfiguration(RootPath, Logger);
            ISMCRTServer Server = new DefaultSMCRTServer(Configuration, RootPath, Logger, null, false);
            Server.Run();
        }
        catch (Exception e)
        {
            Logger.Dispose();
            Logger.Critical($"Server interface crashed! Exception: {e}");
        }


        SMCRTServerOptions Options = new()
        {
            Description = "Default Server",
            ServerName = "LocalServer",
            MaxPlayers = 1,
            ServerAddress = null,
            ServerPort = null
        };
    }


    // Private static methods.
    private static ILogger InitializeLogger(string rootPath)
    {
        string LogsDirectory = Path.Combine(rootPath, DIR_LOGS);
        string LogPath = Path.Combine(LogsDirectory, FILENAME_LOG);

        Directory.CreateDirectory(LogsDirectory);

        if (File.Exists(LogPath))
        {
            ILogArchiver Archiver = new GHLogArchiver();
            string ArchivesDirectory = Path.Combine(LogsDirectory, DIR_LOGS_OLD);
            Directory.CreateDirectory(ArchivesDirectory);
            Archiver.Archive(ArchivesDirectory, LogPath);
        }

        File.Delete(LogPath);
        return new ConsoleLogger(new GHLogger(LogPath));
    }

    private static SMCRTServerOptions LoadConfiguration(string rootPath, ILogger logger)
    {
        string ConfigPath = Path.Combine(rootPath, FILENAME_CONFIG);
        return new ConfigurationReader().ReadConfig(ConfigPath);
    }
}
