using GHEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public record class SMCRTServerOptions
{
    // Fields
    public required IPAddress? ServerAddress { get; init; }
    public required short? ServerPort { get; init; }
    public required string ServerName { get; init; }
    public required string Description { get; init; }
    public required int MaxPlayers { get; init; }
}