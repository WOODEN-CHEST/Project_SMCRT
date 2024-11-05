using Project_SMCRT_Server.Packet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public class DefaultClientPacketProcessor : IClientPacketProcessor
{
    // Private fields.
    private readonly ISMCRTServer _server;

    // Constructors.
    public DefaultClientPacketProcessor(ISMCRTServer server)
    {
        _server = server ?? throw new ArgumentNullException(nameof(server));
    }


    // Inherited methods.
    public void ProcessPacket(ulong playerID, ClientPacket packet)
    {
        throw new NotImplementedException();
    }
}