namespace Cardinal.Service;
using StackExchange.Redis;
using Cardinal.Entities;

public static class Redis
{
    private static IConnectionMultiplexer? conn;
    private static ISubscriber? sub;
    private static ILogger log = new Logger();

    public static void Connect(String connectionString)
    {
        Redis.log.Info("Connecting to Redis", connectionString);
        var connection = ConnectionMultiplexer.Connect(connectionString);
        Redis.conn = (IConnectionMultiplexer)connection;
        while (connection.IsConnecting) {
            Redis.log.Info("Waiting for REDIS connection");
            System.Threading.Thread.Sleep(1000);
        }
        Redis.log.Info("Redis no longer IsConnecting.");

        if (connection.IsConnected) {
            Redis.log.Info("Redis connection is successful!");
        }

        Redis.sub = Redis.conn.GetSubscriber();

        Redis.conn.ConnectionFailed += (object? sender, ConnectionFailedEventArgs args) => {
            Redis.log.Warning("Connection to Redis failed.");
        };

        Redis.conn.ConnectionRestored += (object? sender, ConnectionFailedEventArgs args) => {
            Redis.log.Info("Connection to Redis is successful.");
        };
    }

    public static void Connect(RedisConfig endPoint)
    {
        EndPointCollection e = new EndPointCollection();
        e.Add(endPoint.EndPoints);
        try {
            var connection = ConnectionMultiplexer.Connect(
                new ConfigurationOptions{
                    EndPoints = e,
                    ChannelPrefix = endPoint.ChannelPrefix
                }
            );
            while (connection.IsConnecting) {
                Redis.log.Info("Waiting for REDIS connection");
                System.Threading.Thread.Sleep(1000);
            }
            Redis.log.Info("Redis no longer IsConnecting.");
            Redis.conn = (IConnectionMultiplexer)connection;
            Redis.sub = Redis.conn.GetSubscriber();
        } catch (RedisConnectionException exception) {
            Redis.log.Error(exception.Message);
            throw new Exception(exception.Message);
        }
    }

    public static void Write(string key, string message)
    {
        if (Redis.conn == null) {
            return;
        }
        IDatabase db = Redis.conn.GetDatabase();
        db.StringSet(key, message);
    }

    public static string Read(string key)
    {
        if (Redis.conn == null) {
            return "";
        }
        IDatabase db = Redis.conn.GetDatabase();
        return db.StringGet(key);
    }

    public static void Subscribe(String Topic, Func<ChannelMessage, Task> Callback)
    {
        if (Redis.sub == null) {
            return;
        }
        Redis.sub.Subscribe(Topic).OnMessage(Callback);
    }

    public static void Publish(String Topic, String Message)
    {
        if (Redis.sub == null) {
            return;
        }
        Redis.sub.PublishAsync(Topic, Message);
    }
    public static void Publish(String Topic, Cardinal.Event.AbstractEvent e)
    {
        if (Redis.sub == null) {
            return;
        }
        Redis.sub.PublishAsync(Topic, e.compile());
    }
}