using Project_SMCRT_Server.Packet.Server;
using Project_SMCRT_Server.World.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public class ServerPacketCreator
{
    // Constructors.
    public ServerPacketCreator() { }


    // Private methods.


    // Methods.
    public IEnumerable<ServerPacket> GetPackets(IWorldMonitor world)
    {
        List<ServerPacket> Packets = new();

        

        return Packets;
    }
}