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
        Assert.That(() =>
        {
            IListener Listener = new Listener();
            Listener.Stop();
        }, Throws.Nothing);
    }

    [Test]
    public void Listener_Did_Init()
    {
        Assert.That(() =>
        {
            IListener Listener = new Listener();
            Listener.Stop();
            Assert.That(Listener.IsInit(), Is.True);
        }, Throws.Nothing);
    }

    [Test]
    public void Listener_Can_Listen_For_Connections()
    {

        Assert.That(() =>
        {
            IListener Listener = new Listener(new Cardinal.Entities.Config { IP_ADDRESS = "0.0.0.0", PORT = 7777 });
            Listener.ListenForConnections();
            Thread.Sleep(100);
            Listener.Stop();

        }, Throws.TypeOf<System.Net.Sockets.SocketException>());
    }

}