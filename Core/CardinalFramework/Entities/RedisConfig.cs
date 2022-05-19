namespace Cardinal.Entities;

public class RedisConfig : AbstractEntity
{
    public string? EndPoints { get; set; }
    public string? ChannelPrefix { get; set ; }
}