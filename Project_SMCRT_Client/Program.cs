using Microsoft.Xna.Framework;
using Project_SMCRT_Client.Section;

namespace Project_SMCRT_Client;

internal class Program
{
    static void Main(string[] args)
    {
        using Game MainGame = new SMCRTGame();
        MainGame.Run();
    }
}