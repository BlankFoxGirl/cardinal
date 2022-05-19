namespace Cardinal;
using System;

using Cardinal.Entities;
using Cardinal.Controller;

public class Core
{
    private Config config;
    private IWorker? Worker;
    private IListener? Listener;
    private bool Active = true;
    private Cardinal.Service.ILogger log = new Cardinal.Service.Logger();

    public Core()
    {
        this.config = new Config{
            PORT = 8000,
            IP_ADDRESS = "0.0.0.0",
            IDENTIFIER = "GameServer",
            IS_LISTENER = false
        };

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
        // Workers look items in redis.
        this.log.Verbose("Worker iteration");
    }

    private void StartWorker()
    {
        this.log.Info("Initialising as Worker");
        this.Worker = new Worker();
        this.log.Info("Initialised!");
    }

    private void StartListener()
    {
        this.log.Info("Initialising as Listener");
        this.Listener = new Listener();
        this.log.Info("Initialised!");
    }
}