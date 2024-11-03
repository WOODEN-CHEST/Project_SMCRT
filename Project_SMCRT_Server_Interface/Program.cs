using GHEngine.Logging;

namespace Project_SMCRT_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SMCRTServerOptions Options = new()
            {
                Description = "Default Server",
                ServerName = "LocalServer",
                MaxPlayers = 1,
                ServerAddress = null,
                ServerPort = null
            };


            ILogger Logger = new GHLogger(Console.OpenStandardOutput());
            ISMCRTServer Server = new DefaultSMCRTServer(Options, Logger, true);
            Task.Factory.StartNew(() => Server.Run(), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            while (true)
            {
                Console.ReadKey();
                Server.ScheduleAction(() => Server.IsPaused = !Server.IsPaused);
            }
        }
    }
}
