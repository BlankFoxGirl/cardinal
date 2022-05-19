namespace Cardinal.Workers;
using Cardinal;
using System;

class Worker {
    private static Core? cardinal;
    static void Main()
    {
        System.Console.WriteLine("Starting");
        Worker.cardinal = new Core();
    }
}
