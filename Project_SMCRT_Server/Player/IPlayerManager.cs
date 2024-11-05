using GHEngine;
using Project_SMCRT_Server.Packet;
using Project_SMCRT_Server.Packet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Player;

public interface IPlayerManager : IPacketReceiver
{
    // Fields.
    IEnumerable<ulong> Players { get; }


    // Methods.
    void Start();
    void End();
    void SendPacket(ulong player, ServerPacket packet);
    void KickPlayer(ulong player);
}