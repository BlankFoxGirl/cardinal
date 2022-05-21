namespace Cardinal.Service;

public interface ILogger
{
    public void Error(string message, string payload = "");
    public void Info(string message, string payload = "");
    public void Warning(string message, string payload = "");
    public void Debug(string message, string payload = "");
    public void Verbose(string message, string payload = "");
    public Logger.Level LevelFromString(string? level);
}