using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet.Server;

public abstract class ServerPacket : SMCRTPacket
{
    public ServerPacket(PacketType type) : base(type, PacketSource.Server) { }
}