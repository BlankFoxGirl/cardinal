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
        Cardinal.Core.logLevel = Cardinal.Service.Logger.Level.Debug;
        Assert.That(() => {
            this.logger.Debug("Test debug");
        }, Throws.Nothing);
    }

    [Test]
    public void Logger_Can_Log_Verbose()
    {
        Cardinal.Core.logLevel = Cardinal.Service.Logger.Level.Verbose;
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

    [Test]
    public void Logger_Cannot_Log_Disabled_Level()
    {
        Cardinal.Core.logLevel = Cardinal.Service.Logger.Level.Info;
        Logger l = new Logger();
        Assert.That(
            l.Log("Test info", "", Cardinal.Service.Logger.Level.Verbose),
            Is.EqualTo(false)
        );
    }

    [Test]
    public void Logger_Get_Level_From_String()
    {

        Logger l = new Logger();
        Assert.That(
            l.LevelFromString("0"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Error)
        );
        Assert.That(
            l.LevelFromString("Error"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Error)
        );
        Assert.That(
            l.LevelFromString("1"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Warning)
        );
        Assert.That(
            l.LevelFromString("Warning"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Warning)
        );
        Assert.That(
            l.LevelFromString("2"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Info)
        );
        Assert.That(
            l.LevelFromString("Info"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Info)
        );
        Assert.That(
            l.LevelFromString("3"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Debug)
        );
        Assert.That(
            l.LevelFromString("Debug"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Debug)
        );
        Assert.That(
            l.LevelFromString("4"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Verbose)
        );
        Assert.That(
            l.LevelFromString("Verbose"),
            Is.EqualTo(Cardinal.Service.Logger.Level.Verbose)
        );
    }

    [Test]
    public void Logger_Cannot_Get_Level_From_Null()
    {
        Assert.That(() => {
            this.logger.LevelFromString(null);
        }, Throws.InstanceOf<Cardinal.Exceptions.InvalidLogLevelException>());
    }

    [Test]
    public void Logger_Cannot_Get_Level_From_Invalid()
    {
        Assert.That(() => {
            this.logger.LevelFromString("12312312312");
        }, Throws.InstanceOf<Cardinal.Exceptions.InvalidLogLevelException>());
    }

}