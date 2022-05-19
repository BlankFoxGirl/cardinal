namespace Cardinal.Service;
using StackExchange.Redis;
using Cardinal.Entities;

public static class Redis
{
    private static IConnectionMultiplexer? conn;
    private static ISubscriber? sub;

    public static void Connect(String connectionString)
    {
        var connection = ConnectionMultiplexer.Connect(connectionString);
        Redis.conn = (IConnectionMultiplexer)connection;
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
            Redis.conn = (IConnectionMultiplexer)connection;
            Redis.sub = Redis.conn.GetSubscriber();
        } catch (RedisConnectionException exception) {
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
        Redis.sub.Publish(Topic, Message);
    }
}