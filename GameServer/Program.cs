namespace Cardinal.GameServers;
using Cardinal;
using System;

class GameServer {
    private static Core? cardinal;
    static void Main()
    {
        System.Console.WriteLine("Starting");
        GameServer.cardinal = new Core();
    }
}
