namespace CoreTest.Entity;
using Cardinal.Entities;

public class EventTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Event_Generates_UUID()
    {
        Event e = new Event();
        Assert.True(e.uuid != null);
    }

    [Test]
    public void Event_Getters_And_Setters()
    {
        Event e = new Event{
            key = "test",
            payload = "payload"
        };
        Assert.That(e.key, Is.EqualTo("test"));
        Assert.That(e.payload, Is.EqualTo("payload"));
    }
}