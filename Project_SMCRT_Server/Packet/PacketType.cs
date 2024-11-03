using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public enum PacketType
{
    None = 0,

    Ping,
    Pong,

    ServerMetaQuery,
    ServerMetaResponse
}