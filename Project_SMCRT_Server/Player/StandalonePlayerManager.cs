using GHEngine.IO.GHDF;
using GHEngine.Logging;
using Project_SMCRT_Server.Packet;
using Project_SMCRT_Server.Packet.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Player;

public class StandalonePlayerManager : IPlayerManager
{
    // Fields.
    public IEnumerable<ulong> Players => throw new NotImplementedException();


    // Private fields.
    private readonly Dictionary<ulong, UdpClient> _clients = new();
    private readonly ConcurrentQueue<GHDFCompound> _packetsToSend = new();

    private readonly ILogger? _logger;
    private readonly ISMCRTServer _server;
    private readonly IClientPacketProcessor _packetProcessor;
    private readonly IPAddress _address;
    private readonly short _port;
    private bool _isRunning = false;
    private readonly IGHDFWriter _packetWriter = IGHDFWriter.GetWriterVersion1();
    private readonly ConcurrentQueue<SMCRTPacket> _queuedPackets = new();


    // Constructors.
    public StandalonePlayerManager(ISMCRTServer server, IPAddress address, short port, ILogger? _logger)
    {
        _server = server ?? throw new ArgumentNullException(nameof(server));
        _packetProcessor = new DefaultClientPacketProcessor(server);
        _address = address ?? throw new ArgumentNullException(nameof(_address));
        _port = port;
    }


    // Private methods.  
    private async void HandleTCPClient(TcpClient client)
    {

    }

    private bool IsRunning()
    {
        lock (this)
        {
            return _isRunning;
        }
    }

    private async void RunAsync()
    {
        TcpListener SafeListener = new(_address, _port);
        SafeListener.Start();

        try
        {
            while (IsRunning())
            {
                TcpClient Client = await SafeListener.AcceptTcpClientAsync();
                if (!IsRunning())
                {
                    break;
                }

                HandleTCPClient(Client);
            }
        }
          
        catch (Exception e)
        {
            _logger?.Error($"Player manager crashed! Exception: {e}");
        }
    }

    // Inherited methods.
    public void KickPlayer(ulong player)
    {
        
    }

    public void ReceivePacket(SMCRTPacket packet)
    {
        
    }

    public void SendPacket(ulong player, ServerPacket packet)
    {
        ArgumentNullException.ThrowIfNull(packet, nameof(packet));

        if (!_clients.TryGetValue(player, out UdpClient? Client))
        {
            return;
        }

        GHDFCompound Compound = packet.Encode();
        MemoryStream Stream = new MemoryStream();
        _packetWriter.Write(Stream, Compound);
        byte[] DataToSend = Stream.ToArray();
        Client.Send(DataToSend, DataToSend.Length);
    }

    public void Start()
    {
        _isRunning = true;
        Task.Factory.StartNew(() => RunAsync(), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public void End()
    {
        lock (this)
        {
            _isRunning = false;
        }
    }
}