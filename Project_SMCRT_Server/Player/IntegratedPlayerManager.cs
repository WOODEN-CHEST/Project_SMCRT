using Project_SMCRT_Server.Packet;
using Project_SMCRT_Server.Packet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Player;

public class IntegratedPlayerManager : IPlayerManager
{
    public IEnumerable<ulong> Players { get; } = new ulong[] { 0uL };


    // Private fields.
    private readonly IPacketReceiver _client;
    private readonly IClientPacketProcessor _packetProcessor;


    // Constructors.
    public IntegratedPlayerManager(IPacketReceiver client, IClientPacketProcessor packetProcessor)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _packetProcessor = packetProcessor ?? throw new ArgumentNullException(nameof(packetProcessor));
    }



    // Inherited methods.
    public void KickPlayer(ulong player) { }

    public void ReceivePacket(SMCRTPacket packet) { }

    public void SendPacket(ulong player, ServerPacket packet) { }

    public void Start() { }

    public void End() { }
}