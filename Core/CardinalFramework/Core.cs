namespace Cardinal;
using System;
using System.Collections.Generic;

using Entities;
using Controller;

public class Core
{
    private Config config;
    private IWorker? Worker;
    private IListener? Listener;
    private bool Active = true;
    private Service.ILogger log = new Service.Logger();
    public static Service.Logger.Level logLevel = Service.Logger.Level.Info;
    private List<Controller.Event> eventControllers = new List<Controller.Event>();
    private static List<ClientConnection> clientConnections = new List<ClientConnection>();
    private bool dryRun = false;

    public Core()
    {
        this.config = new Config
        {
            REDIS_PORT = 6379,
            REDIS_HOST = "localhost",
            PORT = 17777,
            IP_ADDRESS = "0.0.0.0",
            IDENTIFIER = "GameServer",
            IS_LISTENER = false
        };

        this.loadVars();

        this.Init();
    }

    public Core(bool dryRun, Config conf)
    {
        this.dryRun = dryRun;
        this.config = conf;

        this.Init();
    }

    public Core(Config conf)
    {
        this.config = conf;

        this.Init();
    }

    public void Init()
    {
        this.log.Info("Starting Cardinal Core.");
        try
        {
            if (this.config.IS_LISTENER == true)
            {
                this.StartListener();
            }
            else
            {
                this.StartWorker();
            }
            StartLoop();
        }
        catch (Exception exception)
        {
            this.log.Debug("Exception identified.");
            this.log.Error(exception.Message);
        }
    }

    /// <summary>Adds a valid connection to the connection pool.</summary>
    /// <param name="connection">ClientConnection to add to the connection pool.</param>
    /// <exception cref="InvalidClientConnectionException">Thrown when supplied ClientConnection is not valid.</exception>
    public static void AddConnection(ClientConnection connection)
    {
        connection.Validate();
        Core.clientConnections.Add(connection);
    }

    /// <summary>Search the connection pool and return a ClientConnection matching the supplied UUID.</summary>
    /// <param name="uuid">UUID of the ClientConnection to retrieve.</param>
    public static ClientConnection GetConnection(string uuid)
    {
        foreach (ClientConnection conn in Core.clientConnections)
        {
            if (conn.uuid == uuid)
            {
                return conn;
            }
        }

        throw new Exceptions.ClientConnectionNotFoundException();
    }

    private void StartLoop()
    {
        this.log.Info("Started! Welcome to Cardinal Framework.");
        while (this.Active && !this.dryRun)
        {
            // Do loop.
            this.Loop();
        }
        this.log.Debug("Loop Terminated.");
    }

    private void Loop()
    {
        if (this.config.IS_LISTENER == true)
        {
            this.ListenerLoop();
        }
        else
        {
            this.WorkerLoop();
        }
    }

    private void ListenerLoop()
    {
        // Listeners look for TCP connections.
        this.log.Verbose("Listener iteration");
        System.Threading.Thread.Sleep(100);
    }

    private void WorkerLoop()
    {
        this.log.Verbose("Worker iteration");
        // Workers look items in redis.
        System.Threading.Thread.Sleep(5000);
    }

    private void StartWorker()
    {
        this.log.Info("Initialising as Worker");
        this.Worker = new Worker(this.config);
        var workerThread = new Controller.Event("Worker");
        this.eventControllers.Add(workerThread);
        this.log.Info("Worker Initialised!");
    }

    private void StartListener()
    {
        this.log.Info("Initialising as Listener");
        var listenerThread = new Controller.Event("Response");
        this.eventControllers.Add(listenerThread);
        this.Listener = new Listener(this.config);
        this.log.Info("Listener Initialised!");
    }

    private void loadVars()
    {
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_HOST")))
        {
            this.config.REDIS_HOST = Environment.GetEnvironmentVariable("REDIS_HOST");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_PORT")))
        {
            this.config.REDIS_PORT = int.Parse(Environment.GetEnvironmentVariable("REDIS_PORT") ?? "0");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GS_PORT")))
        {
            this.config.PORT = int.Parse(Environment.GetEnvironmentVariable("GS_PORT") ?? "0");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IDENTIFIER")))
        {
            this.config.IDENTIFIER = Environment.GetEnvironmentVariable("IDENTIFIER");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IS_LISTENER")))
        {
            this.config.IS_LISTENER = Environment.GetEnvironmentVariable("IS_LISTENER") == "true";
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_LEVEL")))
        {
            Service.Logger.Level level = this.log.LevelFromString((string?)Environment.GetEnvironmentVariable("LOG_LEVEL"));
            Core.logLevel = level;
        }
    }
}