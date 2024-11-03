using GHEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server;

public class SMCRTServer
{
    // Fields.
    public ILogger? Logger { get; private init; }
    public bool IsInternalServer { get; private init; }


    // Constructors.
    public SMCRTServer(SMCRTServerOptions options, ILogger? logger = null, bool isInternalServer = false)
    {
        Logger = logger;
        IsInternalServer = isInternalServer;
    }
}