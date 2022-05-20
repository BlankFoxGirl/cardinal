namespace CoreTest.Controller;
using Cardinal.Controller;

public class EventTest
{
    Mock<Cardinal.Service.ILogger>? mockLogger;
    [SetUp]
    public void Setup()
    {
        this.mockLogger = new Mock<Cardinal.Service.ILogger>();
        this.mockLogger.Setup(log => log.Debug("Received Message: ", "test"));
        this.mockLogger.Setup(log => log.Verbose("Exited.", ""));
    }

    [Test]
    public void Event_Is_Init()
    {
        Event e = new Event();
        Assert.That(e.IsInit(), Is.EqualTo(true));
    }

    [Test]
    public void Event_Can_Run_OnReceive()
    {
        string eventMessage = "0x01:test{|}0x02:test{|}0x03:test";
        Event e = new Event();
        Assert.That(e.IsInit(), Is.EqualTo(true));
        Assert.That(() => e.OnReceive(eventMessage), Throws.InstanceOf<System.IO.FileNotFoundException>());
    }

    [Test]
    public void Event_Can_Run_OnReceive_But_Exits_No_target()
    {
        string eventMessage = "0x01:test{|}0x02:{|}0x03:test";
        IEvent e = new Event();
        Assert.That(e.IsInit(), Is.EqualTo(true));
        Assert.That(() => e.OnReceive(eventMessage), Throws.InstanceOf<Cardinal.Exceptions.InvalidEventException>());
    }
}