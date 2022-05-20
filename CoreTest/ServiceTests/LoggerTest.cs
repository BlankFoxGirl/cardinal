namespace CoreTest.Service;
using Cardinal.Service;

public class LoggerTest
{
    ILogger logger = new Logger();
    [SetUp]
    public void SetUp()
    { }

    [Test]
    public void Logger_Can_Log_Error()
    {
        Assert.That(() => {
            this.logger.Error("Test error");
        }, Throws.Nothing);
    }

    [Test]
    public void Logger_Can_Log_Warning()
    {
        Assert.That(() => {
            this.logger.Warning("Test warning");
        }, Throws.Nothing);
    }

    [Test]
    public void Logger_Can_Log_Debug()
    {
        Assert.That(() => {
            this.logger.Debug("Test debug");
        }, Throws.Nothing);
    }

    [Test]
    public void Logger_Can_Log_Verbose()
    {
        Assert.That(() => {
            this.logger.Verbose("Test verbose");
        }, Throws.Nothing);
    }

    [Test]
    public void Logger_Can_Log_Info()
    {
        Assert.That(() => {
            this.logger.Info("Test info");
        }, Throws.Nothing);
    }

}