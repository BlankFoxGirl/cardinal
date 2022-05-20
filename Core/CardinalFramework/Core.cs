namespace Cardinal;
using System;
using System.Collections.Generic;

using Cardinal.Entities;
using Cardinal.Controller;

public class Core
{
    private Config config;
    private IWorker? Worker;
    private IListener? Listener;
    private bool Active = true;
    private Cardinal.Service.ILogger log = new Cardinal.Service.Logger();
    private List<Cardinal.Controller.Event> eventControllers = new List<Controller.Event>();

    public Core()
    {
        this.config = new Config{
            PORT = 8000,
            IP_ADDRESS = "0.0.0.0",
            IDENTIFIER = "GameServer",
            IS_LISTENER = false
        };

        this.loadVars();

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
        try {
            if (this.config.IS_LISTENER == true) {
                this.StartListener();
            } else {
                this.StartWorker();
            }
            StartLoop();
        } catch (Exception exception) {
            this.log.Debug("Exception identified.");
            this.log.Error(exception.Message);
        }
    }

    private void StartLoop()
    {
        this.log.Info("Started! Welcome to Cardinal Framework.");
        while (this.Active) {
            // Do loop.
            this.Loop();
        }
        this.log.Debug("Loop Terminated.");
    }

    private void Loop()
    {
        if (this.config.IS_LISTENER == true) {
            this.ListenerLoop();
        } else {
            this.WorkerLoop();
        }
    }

    private void ListenerLoop()
    {
        // Listeners look for TCP connections.
        this.log.Verbose("Listener iteration");
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
        var workerQueue = new Controller.Event("Worker");
        this.eventControllers.Add(workerQueue);
        this.log.Info("Worker Initialised!");
    }

    private void StartListener()
    {
        this.log.Info("Initialising as Listener");
        this.Listener = new Listener();
        this.log.Info("Listener Initialised!");
    }

    private void loadVars()
    {
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_HOST"))) {
            this.config.IP_ADDRESS = Environment.GetEnvironmentVariable("REDIS_HOST");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_PORT"))) {
            this.config.PORT = int.Parse(Environment.GetEnvironmentVariable("REDIS_PORT") ?? "0");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IDENTIFIER"))) {
            this.config.IDENTIFIER = Environment.GetEnvironmentVariable("IDENTIFIER");
        }

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IS_LISTENER"))) {
            this.config.IS_LISTENER = Environment.GetEnvironmentVariable("IS_LISTENER") == "true";
        }
    }
}