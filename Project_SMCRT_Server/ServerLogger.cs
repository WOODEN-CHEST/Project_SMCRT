using GHEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public class ServerLogger : ILogger
{
    // Static fields.
    public const string PREFIX = "(Server)";


    // Fields.
    public event EventHandler<LoggerLogEventArgs>? LogMessage;


    // Private fields.
    private readonly ILogger _baseLogger;


    // Constructors.
    public ServerLogger(ILogger baseLogger)
    {
        _baseLogger = baseLogger ?? throw new ArgumentNullException(nameof(baseLogger));
    }


    // Private methods.
    private string ProcessMessage(string message)
    {
        return $"{PREFIX} {message}";
    }

    // Inherited methods.
    public string ConvertToLoggedMessage(LogLevel level, DateTime timeStamp, string message)
    {
        return _baseLogger.ConvertToLoggedMessage(level, timeStamp, ProcessMessage(message));
    }

    public void Critical(string message)
    {
        Log(LogLevel.CRITICAL, message);
    }

    public void Error(string message)
    {
        Log(LogLevel.Error, message);
    }

    public void Info(string message)
    {
        Log(LogLevel.Info, message);
    }

    public void Warning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    public void Log(LogLevel level, string message)
    {
        _baseLogger.Log(level, ProcessMessage(message));
    }

    public void Dispose()
    {
        _baseLogger.Dispose();
    }
}