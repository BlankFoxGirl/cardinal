namespace CoreTest.Controller;
using Cardinal.Controller;

public class WorkerTest
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
    public void Worker_Can_Init()
    {
        Assert.That(() => {
            IWorker worker = new Worker();
        }, Throws.Nothing);
    }

    [Test]
    public void Worker_Did_Init()
    {
        IWorker worker = new Worker();
        Assert.That(worker.IsInit(), Is.True);
    }

    [Test]
    public void Worker_Can_Init_With_Config()
    {
        Cardinal.Entities.Config conf = new Cardinal.Entities.Config{
            IP_ADDRESS = "0.0.0.0",
            PORT = 2323
        };
        Assert.That(() => {
            IWorker worker = new Worker(conf);
        }, Throws.Nothing);
    }

    [Test]
    public void Worker_Did_Init_With_Config()
    {
        Cardinal.Entities.Config conf = new Cardinal.Entities.Config{
            IP_ADDRESS = "0.0.0.0",
            PORT = 2323
        };

        IWorker worker = new Worker(conf);
        Assert.That(worker.IsInit(), Is.True);
    }
}