using GHEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server_Interface;

public class ConsoleLogger : ILogger
{
    // Fields.
    public event EventHandler<LoggerLogEventArgs>? LogMessage;


    // Private fields.
    private readonly ILogger _baseLogger;


    // Constructors.
    public ConsoleLogger(ILogger baseLogger)
    {
        _baseLogger = baseLogger ?? throw new ArgumentNullException(nameof(baseLogger));
    }


    // Inherited methods.
    public string ConvertToLoggedMessage(LogLevel level, DateTime timeStamp, string message)
    {
        return _baseLogger.ConvertToLoggedMessage(level, timeStamp, message);
    }

    public void Critical(string message)
    {
        Log(LogLevel.CRITICAL, message);
    }

    public void Dispose()
    {
        _baseLogger.Dispose();
    }

    public void Error(string message)
    {
        Log(LogLevel.Error, message);
    }

    public void Info(string message)
    {
        Log(LogLevel.Info, message);
    }

    public void Log(LogLevel level, string message)
    {
        LoggerLogEventArgs Args = new(level, message, DateTime.Now);
        LogMessage?.Invoke(this, Args);

        Console.ForegroundColor = Args.Level switch
        {
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.CRITICAL => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };

        Console.Write(ConvertToLoggedMessage(Args.Level, Args.TimeStamp, Args.Message));
        _baseLogger.Log(Args.Level, Args.Message);
    }

    public void Warning(string message)
    {
        Log(LogLevel.Warning, message);
    }
}