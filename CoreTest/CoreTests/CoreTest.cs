namespace CoreTest;

public class CoreTest
{
    Mock<Cardinal.Service.ILogger>? mockLogger;
    Mock<Cardinal.Controller.IWorker>? mockWorker;
    [SetUp]
    public void Setup()
    {
        this.mockLogger = new Mock<Cardinal.Service.ILogger>();
        this.mockWorker = new Mock<Cardinal.Controller.IWorker>();
        // this.mockWorker.Setup(worker => worker.init());
        this.mockLogger.Setup(log => log.Debug("Received Message: ", "test"));
        this.mockLogger.Setup(log => log.Verbose("Exited.", ""));
    }

    // [Ignore("Cannot test due to requiring a valid socket connection.")]
    [Test]
    public void Core_Can_DryRun_Listener()
    {
        Cardinal.Entities.Config conf = new Cardinal.Entities.Config{
            IP_ADDRESS = "0.0.0.0",
            PORT = 123,
            REDIS_HOST = "redis",
            REDIS_PORT = 6379,
            IDENTIFIER = "DryRunTest"
        };
        Assert.That(() => {
            Core core = new Core(true, conf);
        }, Throws.Nothing);
    }

    [Test]
    public void Core_Can_DryRun_Worker()
    {
        Cardinal.Entities.Config conf = new Cardinal.Entities.Config{
            REDIS_HOST = "redis",
            REDIS_PORT = 6379,
            IDENTIFIER = "DryRunTest",
            IS_LISTENER = false
        };
        Assert.That(() => {
            Core core = new Core(true, conf);
        }, Throws.Nothing);
    }

    [Ignore("Cannot test due to requiring a valid socket connection.")]
    [Test]
    public void Core_Can_Get_Connection()
    {
        Cardinal.Entities.ClientConnection cc = new Cardinal.Entities.ClientConnection();
        Cardinal.Core.AddConnection(cc);
        Assert.That(() =>
        {
            Cardinal.Core.GetConnection(cc.uuid);
        }, Throws.Nothing);
    }
}