using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet.Client;

public abstract class ClientPacket : SMCRTPacket
{
    public ClientPacket(PacketType type) : base(type, PacketSource.Client) { }
}