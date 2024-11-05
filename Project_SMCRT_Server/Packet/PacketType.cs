using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Packet;

public enum PacketType
{
    None = 0,

    // Out of Game
    ServerMetaQuery,
    ServerMetaResponse,

    // Login.
    ServerJoinRequest,
    ServerJoinResponse,
    ServerKick,

    ContentRequest,
    ContentLoad,

    // In-Game
    Ping,
    Pong,

    ClientChat,
    ChatMessageShow,


    EntityInitialize,
    InitializeFinish,

    ComponentChange,
    EntityChange,

    UserInput,
    SpecialRequest,
    ServerSpecialEvent
}