namespace CoreTest.Entity;
using Cardinal.Entities;

public class RedisConfigTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RedisConfig_Generates_UUID()
    {
        RedisConfig rc = new RedisConfig();
        Assert.True(rc.uuid != null);
    }

    [Test]
    public void RedisConfig_Getters_And_Setters()
    {
        RedisConfig rc = new RedisConfig{
            EndPoints = "test",
            ChannelPrefix = "prefix"
        };
        Assert.That(rc.EndPoints, Is.EqualTo("test"));
        Assert.That(rc.ChannelPrefix, Is.EqualTo("prefix"));
    }
}