namespace CoreTest.Entity;
using Cardinal.Entities;

public class ConfigTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Config_Generates_UUID()
    {
        Config conf = new Config();
        Assert.True(conf.uuid != null);
    }

    [Test]
    public void Config_Defaults_To_Listener()
    {
        Config conf = new Config();
        Assert.True(conf.IS_LISTENER);
    }

    [Test]
    public void Config_Setters_And_Getters()
    {
        Config conf = new Config{
            IS_LISTENER = false,
            PORT = 1234,
            IP_ADDRESS = "1.1.1.1",
            IDENTIFIER = "test",
            Queue = "test"
        };

        Assert.That(conf.IS_LISTENER, Is.EqualTo(false));
        Assert.That(conf.PORT, Is.EqualTo(1234));
        Assert.That(conf.IP_ADDRESS, Is.EqualTo("1.1.1.1"));
        Assert.That(conf.IDENTIFIER, Is.EqualTo("test"));
        Assert.That(conf.Queue, Is.EqualTo("test"));
    }
}