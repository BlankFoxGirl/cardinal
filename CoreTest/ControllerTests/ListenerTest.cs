namespace CoreTest.Controller;
using Cardinal.Controller;

public class ListenerTest
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
    public void Listener_Can_Init()
    {
        Assert.That(() => {
            IListener Listener = new Listener();
        }, Throws.Nothing);
    }

    [Test]
    public void Listener_Did_Init()
    {
        IListener Listener = new Listener();
        Assert.That(Listener.IsInit(), Is.True);
    }
}