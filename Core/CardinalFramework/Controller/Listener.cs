namespace Cardinal.Controller;

using System.Net;
using System.Net.Sockets;
using Service;
using System.Text;
using System.Threading;

public class Listener : AbstractController, IListener
{
    private TcpListener? server;
    private ILogger log = new Logger();
    public Listener() : base()
    {
        this.init();
    }
    public Listener(Entities.Config conf) : base()
    {
        this.Conf = conf;
        this.connectToRedis();
        this.startTcpServer();
    }
    public override void init()
    {
        base.init(); // Call parent init.
    }

    private void connectToRedis()
    {
        string connectionString = "localhost,abortConnect=false";
        if (this.Conf != null) {
            connectionString = this.Conf.REDIS_HOST + ":" + this.Conf.REDIS_PORT + ",abortConnect=false";
        }

        Redis.Connect(connectionString);
    }

    private void startTcpServer()
    {
        if (this.Conf == null)
        {
            this.Conf = new Entities.Config();
            this.Conf.PORT = 7777;
        }

        if (this.server == null) {
            string ip = this.Conf.IP_ADDRESS ?? "";
            IPAddress IP;

            if (string.IsNullOrEmpty(ip) || ip == "0.0.0.0") {
                IP = IPAddress.Any;
            } else {
                IP = IPAddress.Parse(ip);
            }
            if (this.Conf.PORT == null) {
                this.Conf.PORT = 7777;
            }
            this.log.Info("Binding to", string.Format("{0}:{1}", IP.ToString(), this.Conf.PORT.ToString()));

            TcpListener server = new TcpListener(IP, this.Conf.PORT ?? 8000);
            this.server = server;
        }
        this.server.Stop();
        this.server.Start();
    }

    public void ListenForConnections()
    {
        if (this.server == null)
        {
            throw new Exceptions.TcpListenerNotFoundException();
        }
        if (this.server.Pending()) {
            return;
        }

        this.log.Debug("Waiting for a connection...");
        TcpClient client = this.server.AcceptTcpClient();
        this.log.Debug("Connected!");
        Thread t = new Thread(new ParameterizedThreadStart(handleConnection));
        t.Start(client);
    }

    public void Stop()
    {
        if (this.server != null)
        {
            this.server.Stop();
        }
    }

    private void handleConnection(object? o)
    {
        if (o == null)
        {
            throw new Exceptions.InvalidConnectionAttempt();
        }
        Entities.ClientConnection conn = new Entities.ClientConnection();
        TcpClient client = (TcpClient)o;
        conn.setClient(client);
        conn.setStream(client.GetStream());
    }

    private void readStream(Entities.ClientConnection conn)
    {
        StringBuilder data = new StringBuilder();
        Byte[] bytes = new Byte[256];
        int i;
        try
        {
            while ((i = conn.getStream().Read(bytes, 0, bytes.Length)) != 0)
            {
                data.Append(Encoding.UTF8.GetString(bytes, 0, i));
                if (data.ToString().IndexOf("\n") != -1 || data.ToString().IndexOf("\r") != -1)
                {
                    this.dispatchEvent((data.ToString()).Replace("\n", "").Replace("\r", ""), conn);
                    data.Clear();
                }
            }
        }
        catch (Exception e)
        {
            this.log.Error("Error when trying to read stream.", e.ToString());
            conn.getClient().Close();
        }
    }

    private void dispatchEvent(string stringEvent, Entities.ClientConnection conn)
    {
        try
        {
            Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent(stringEvent);
            Service.Redis.Publish("Worker", ae.compile());
        }
        catch (Exceptions.InvalidMessageException e)
        {
            this.log.Debug("Invalid message received from client " + conn.uuid, e.Message);
        }
        catch (Exception e)
        {
            this.log.Error("Error when attempting to dispatch event from listener", e.ToString());
        }

    }
}