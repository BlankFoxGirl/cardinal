namespace Cardinal.Service;
using System;
public class Logger : ILogger
{
    public enum Level
    {
        Error,
        Warning,
        Info,
        Debug,
        Verbose
    }

    public void Log(string message, string payload = "", Level logLevel = Level.Info)
    {
        this.write(message, payload, logLevel);
    }

    public void Error(string message, string payload = "")
    {
        this.Log(message, payload, Level.Error);
    }

    public void Info(string message, string payload = "")
    {
        this.Log(message, payload, Level.Info);
    }

    public void Warning(string message, string payload = "")
    {
        this.Log(message, payload, Level.Warning);
    }

    public void Debug(string message, string payload = "")
    {
        this.Log(message, payload, Level.Debug);
    }

    public void Verbose(string message, string payload = "")
    {
        this.Log(message, payload, Level.Verbose);
    }

    private void write(string message, string payload = "", Level level = Level.Info)
    {
        string prefix = "";
        switch (level) {
            case Level.Error:
                prefix = "[Error]";
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Level.Warning:
                prefix = "[Warning]";
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Level.Debug:
                prefix = "[Debug]";
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case Level.Verbose:
                prefix = "[Verbose]";
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            default:
                prefix = "[Info]";
                Console.ForegroundColor = ConsoleColor.White;
            break;
        }
        if (string.IsNullOrEmpty(payload)) {
            Console.WriteLine("\t" + prefix + message);
        } else {
            Console.WriteLine("\t" + prefix + message + " -> " + payload);
        }

        Console.ResetColor();
    }
}